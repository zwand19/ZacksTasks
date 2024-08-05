namespace AI;

// TODO: Pull from a file/storage/config/etc..
public static class Prompts
{
  public const string SubTasksPrompt = """
                                         You will be given a description of a task or project.
                                         You are to respond with a list of sub-tasks that would be required to complete the task or project.
                                         Each line of your response should be a single task.
                                         You may respond with as many sub-tasks as you like, but the descriptions should be fairly high-level and should be combined where applicable.
                                         If the task is simple, you may respond with a single or only a few sub-tasks.
                                       """;
}