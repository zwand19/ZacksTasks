using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace AI.OpenAI;

public class OpenAIClient : IOpenAIClient
{
  private readonly string _apiKey;
  
  private readonly string _model;
  
  public bool IsEnabled { get; }
  
  public OpenAIClient(IConfiguration configuration)
  {
    _apiKey = configuration.GetSection("OpenAI")["ApiKey"] ?? "";
    _model = configuration.GetSection("OpenAI")["Model"]?.Trim() ?? "";
    if (_model == "")
    {
      _model = "gpt-4o-mini";
    }
    
    IsEnabled = !string.IsNullOrEmpty(_apiKey);
  }

  public async Task<string[]> GetResponseLines(string[] prompts)
  {
    if (!IsEnabled)
    {
      return Array.Empty<string>();
    }

    var client = new ChatClient(_model, _apiKey);

    var chatCompletion = await client.CompleteChatAsync(prompts.Select(p => new UserChatMessage(p)));
    var textContent = chatCompletion.Value.Content.Where(c => c.Kind == ChatMessageContentPartKind.Text);
    
    // return each line, removing the '-' bullet points that ChatGPT typically adds
    return textContent.SelectMany(c => c.Text.Split('\n').Select(t => t.TrimStart('-').Trim())).ToArray();
  }
}