using Models;

namespace Repositories.Tasks;

public interface ITasksRepository
{
  Task Delete(int id);
  
  Task<ZackTask> Create(ZackTask task);

  Task<IList<ZackTask>> GetAll();
}