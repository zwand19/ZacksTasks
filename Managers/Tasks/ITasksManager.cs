using Models;

namespace Managers.Tasks;

public interface ITasksManager
{
  Task Delete(int id);
  
  Task<ZackTask> Create(ZackTask task);

  Task<IList<ZackTask>> GetAll();
}