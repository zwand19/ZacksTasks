using System.ComponentModel.DataAnnotations;
using Managers.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace OriginTest.Controllers;

[ApiController]
[Route("api/v1/tasks")]
public class TasksController : ControllerBase
{
  private readonly ITasksManager _tasksManager;

  public TasksController(ITasksManager tasksManager)
  {
    _tasksManager = tasksManager;
  }

  [HttpGet]
  [Route("")]
  public Task<IList<ZackTask>> GetAll()
  {
    return _tasksManager.GetAll();
  }

  [HttpPost]
  [Route("")]
  public Task<ZackTask> Create([FromBody] ZackTask task)
  {
    return _tasksManager.Create(task);
  }

  [HttpPost]
  [Route("breakdown")]
  public Task<ZackTask[]> CreateSubTasks([FromBody] ZackTask task)
  {
    if (string.IsNullOrWhiteSpace(task.Description))
    {
      throw new ValidationException();
    }
    
    return _tasksManager.CreateSubTasks(task.Description);
  }

  [HttpDelete]
  [Route("{id:int}")]
  public Task Delete(int id)
  {
    return _tasksManager.Delete(id);
  }

  [HttpDelete]
  public Task DeleteAll()
  {
    return _tasksManager.DeleteAll();
  }
}