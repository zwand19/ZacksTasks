using Models;

namespace Managers.Tasks;

public interface ITasksManager
{
  Task Delete(int id);
  
  Task DeleteAll();
  
  Task<ZackTask> Create(ZackTask task);

  Task<IList<ZackTask>> GetAll();
  
  Task<ZackTask[]> CreateSubTasks(string taskDescription);
}