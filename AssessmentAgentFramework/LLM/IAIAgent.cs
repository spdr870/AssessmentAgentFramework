
namespace AssessmentAgentFramework.LLM
{
	internal interface IAIAgent
	{
		Task<string> RunPromptAsync(string sessionId, string userInput);
	}
}