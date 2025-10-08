using AssessmentAgentFramework.Settings;
using OpenAI;
using OpenAI.Chat;
using System.Text;

public class OpenAiPromptClient
{
	public const string Model = "gpt-4o-2024-08-06";
	private readonly ChatClient _chatClient;

	public OpenAiPromptClient(OpenAISettings openAISettings)
	{
		_chatClient = new OpenAIClient(openAISettings.OpenAIApiKey)
							.GetChatClient(Model);
	}

	public async Task<string> RunPromptAsync(string prompt, string text)
	{
		var messages = new List<ChatMessage>
		{
			new SystemChatMessage(prompt),
			new UserChatMessage(text)
		};

		StringBuilder response = new StringBuilder();
		await foreach (var chunk in _chatClient.CompleteChatStreamingAsync(messages))
		{
			foreach (ChatMessageContentPart part in chunk.ContentUpdate)
			{
				if (!string.IsNullOrEmpty(part.Text))
				{
					response.Append(part.Text);
				}
			}
		}
		return response.ToString();
	}

	public async Task<string> RunPromptOnFileAsync(string prompt, Stream fileStream)
	{
#pragma warning disable OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
		var messages = new List<ChatMessage>
		{
			new SystemChatMessage(prompt),
			new UserChatMessage(ChatMessageContentPart.CreateFilePart(BinaryData.FromStream(fileStream), "application/pdf", "document"))
		};
#pragma warning restore OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

		StringBuilder response = new StringBuilder();
		await foreach (var chunk in _chatClient.CompleteChatStreamingAsync(messages))
		{
			foreach (ChatMessageContentPart part in chunk.ContentUpdate)
			{
				if (!string.IsNullOrEmpty(part.Text))
				{
					response.Append(part.Text);
				}
			}
		}
		return response.ToString();
	}

	public async Task<string> RunPromptOnImageAsync(string prompt, Stream imageStream)
	{
		var messages = new List<ChatMessage>
		{
			new SystemChatMessage(prompt),
			new UserChatMessage(ChatMessageContentPart.CreateImagePart(BinaryData.FromStream(imageStream), "image/jpeg"))
		};

		StringBuilder response = new StringBuilder();
		await foreach (var chunk in _chatClient.CompleteChatStreamingAsync(messages))
		{
			foreach (ChatMessageContentPart part in chunk.ContentUpdate)
			{
				if (!string.IsNullOrEmpty(part.Text))
				{
					response.Append(part.Text);
				}
			}
		}
		return response.ToString();
	}
}