namespace AI;

public interface IAITaskCreator
{
  Task<string[]> GetSubTasks(string taskDescription);
}