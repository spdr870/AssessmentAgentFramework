using Azure.AI.Agents.Persistent;
using Azure.Core;
using AssessmentAgentFramework.LLM;
using AssessmentAgentFramework.Settings;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Azure;
using MongoDB.Bson;
using OpenAI;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

#pragma warning disable MEAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

public class AssessmentAgentFrameworkAgent : IAIAgent
{
	private readonly AIAgent _agent;

	private readonly ConcurrentDictionary<string, AgentThread> _agentThreads = new();

	public AIAgent ChatCompletionAgent { get {return _agent; } }

	public AssessmentAgentFrameworkAgent(AIAgent agent)
	{ 
		_agent = agent; 
	}

	public AssessmentAgentFrameworkAgent(OpenAISettings openAISettings,
										string name,
										string description,
										string instructions,
										object[] tools)
	{
		IList<AITool> aiFunctions = new List<AITool>();
		foreach(var tool in tools) {
			var methods = tool.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			foreach (var method in methods) {
				AIFunction aiFuntion = AIFunctionFactory.Create(method, tool);

				aiFunctions.Add(aiFuntion);
			}
		}

		_agent = new OpenAIClient(openAISettings.OpenAIApiKey)
							.GetChatClient("gpt-4o-mini")
							.CreateAIAgent(instructions: instructions, 
											description: description, 
											name: name, 
											tools: aiFunctions);
	}

	public async Task<string> RunPromptAsync(string sessionId, string userInput)
	{
		AgentThread agentThread = _agentThreads.GetOrAdd(sessionId, _ => _agent.GetNewThread());

		ChatMessage message = new(ChatRole.User, [
													new TextContent(userInput)
												]);

		AgentRunResponse response = await _agent.RunAsync(message, agentThread);
		
		return response.ToString();
	}
}