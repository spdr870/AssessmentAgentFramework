using AssessmentAgentFramework.AgentTools;
using AssessmentAgentFramework.Settings;
using Microsoft.Extensions.Configuration;

// Setup Configuration
var configuration = new ConfigurationBuilder()
	.AddEnvironmentVariables()
	.SetBasePath(AppContext.BaseDirectory)
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();

Console.OutputEncoding = System.Text.Encoding.UTF8;

OpenAISettings openAISettings = new OpenAISettings();
openAISettings.OpenAIApiKey = configuration["OpenAIApiKey"];

AssessmentAgentFrameworkAgent bakerAgent = new AssessmentAgentFrameworkAgent(
			openAISettings,
			name: "BakerAgent",
			description: "You can bake",
			instructions: "Bake",
			tools: [ new BakeryTool() ]);

string sessionId = Guid.NewGuid().ToString();

// Basic console based chat
while (true)
{
	Console.ForegroundColor = ConsoleColor.White;
	Console.WriteLine("Enter search text");
	Console.ForegroundColor = ConsoleColor.Yellow;
	string searchText = Console.ReadLine();

	string agentResponse = bakerAgent.RunPromptAsync(sessionId, searchText).Result;
	Console.WriteLine(agentResponse);
}