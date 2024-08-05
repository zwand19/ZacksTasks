namespace AI.OpenAI;

public interface IOpenAIClient
{
  Task<string[]> GetResponseLines(string[] prompts);

  bool IsEnabled { get; }
}