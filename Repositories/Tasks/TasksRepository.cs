using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories.Tasks;

public class TasksRepository : ITasksRepository
{
  private readonly ZackTasksDbContext _context;
  private readonly IRepositoryUtility<ZackTask> _repositoryUtility;

  public TasksRepository(ZackTasksDbContext context, IRepositoryUtility<ZackTask> repositoryUtility)
  {
    _context = context;
    _repositoryUtility = repositoryUtility;
  }
  
  public async Task Delete(int id)
  {
    var zackTask = new ZackTask {Id = id, Description = ""};
    _context.Tasks.Attach(zackTask);
    _context.Tasks.Remove(zackTask);
    await _context.SaveChangesAsync();
  }

  public Task DeleteAll()
  {
    return _context.Tasks.ExecuteDeleteAsync();
  }

  public Task Create(ZackTask task)
  {
    return Create(new[] {task});
  }

  public async Task Create(ZackTask[] tasks)
  {
    _repositoryUtility.PrepForInsert(tasks);

    _context.Tasks.AddRange(tasks);
    await _context.SaveChangesAsync();
  }

  public async Task<IList<ZackTask>> GetAll()
  {
    return await _context.Tasks.ToArrayAsync();
  }
}