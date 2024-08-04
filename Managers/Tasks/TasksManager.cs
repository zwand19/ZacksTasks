using Models;
using Repositories.Tasks;

namespace Managers.Tasks;

public class TasksManager : ITasksManager
{
  private readonly ITasksRepository _tasksRepository;
  private const int MinDescriptionLength = 2;
  private const int MaxDescriptionLength = 1000;

  public TasksManager(ITasksRepository tasksRepository)
  {
    _tasksRepository = tasksRepository;
  }
  
  public Task Delete(int id)
  {
    return _tasksRepository.Delete(id);
  }

  public Task<ZackTask> Create(ZackTask task)
  {
    Validate(task);
    
    return _tasksRepository.Create(task);
  }

  // TODO: create a validator class/utilize a validation library
  private static void Validate(ZackTask task)
  {
    if (task == null)
    {
      throw new ArgumentException("Task is null");
    }

    switch (task.Description.Trim().Length)
    {
      case < MinDescriptionLength:
        throw new ArgumentException($"Description must be at least {MinDescriptionLength} characters");
      case > MaxDescriptionLength:
        throw new ArgumentException($"Description must be at most {MaxDescriptionLength} characters");
    }
  }

  public Task<IList<ZackTask>> GetAll()
  {
    return _tasksRepository.GetAll();
  }
}