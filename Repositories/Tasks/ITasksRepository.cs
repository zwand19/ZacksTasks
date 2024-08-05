using Models;

namespace Repositories.Tasks;

public interface ITasksRepository
{
  Task Delete(int id);
  
  Task DeleteAll();
  
  Task Create(ZackTask task);
  
  Task Create(ZackTask[] tasks);

  Task<IList<ZackTask>> GetAll();
}