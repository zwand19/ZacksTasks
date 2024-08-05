using AI.OpenAI;

namespace AI;

public class AITaskCreator : IAITaskCreator
{
  private readonly IOpenAIClient _openAIClient;

  public AITaskCreator(IOpenAIClient openAIClient)
  {
    _openAIClient = openAIClient;
  }
  
  public Task<string[]> GetSubTasks(string taskDescription)
  {
    return _openAIClient.GetResponseLines(new[] {Prompts.SubTasksPrompt, taskDescription});
  }
}