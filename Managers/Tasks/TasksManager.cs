using System.ComponentModel.DataAnnotations;
using AI;
using Models;
using Repositories.Tasks;

namespace Managers.Tasks;

public class TasksManager : ITasksManager
{
  private readonly ITasksRepository _tasksRepository;
  private readonly IAITaskCreator _aiTaskCreator;
  private const int MinDescriptionLength = 2;
  private const int MaxDescriptionLength = 1000;

  public TasksManager(ITasksRepository tasksRepository, IAITaskCreator aiTaskCreator)
  {
    _tasksRepository = tasksRepository;
    _aiTaskCreator = aiTaskCreator;
  }
  
  public Task Delete(int id)
  {
    return _tasksRepository.Delete(id);
  }
  
  public Task DeleteAll()
  {
    return _tasksRepository.DeleteAll();
  }

  public async Task<ZackTask> Create(ZackTask task)
  {
    Validate(task);
    await _tasksRepository.Create(task);
    return task;
  }

  // TODO: create a validator class/utilize a validation library
  private static void Validate(ZackTask task)
  {
    if (task == null)
    {
      throw new ValidationException("Task is null");
    }

    switch (task.Description.Trim().Length)
    {
      case < MinDescriptionLength:
        throw new ValidationException($"Description must be at least {MinDescriptionLength} characters");
      case > MaxDescriptionLength:
        throw new ValidationException($"Description must be at most {MaxDescriptionLength} characters");
    }
  }

  public Task<IList<ZackTask>> GetAll()
  {
    return _tasksRepository.GetAll();
  }

  public async Task<ZackTask[]> CreateSubTasks(string taskDescription)
  {
    var subTasks = await _aiTaskCreator.GetSubTasks(taskDescription);
    if (!subTasks.Any())
    {
      return Array.Empty<ZackTask>();
    }
    
    var tasks = subTasks.Select(t => new ZackTask {Description = t, DateCreated = DateTime.UtcNow}).ToArray();
    await _tasksRepository.Create(tasks);
    
    return tasks;
  }
}