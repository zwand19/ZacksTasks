using Models;

namespace Repositories;

public class RepositoryUtility<T> : IRepositoryUtility<T> where T : BaseEntity
{
  public void PrepForInsert(T entity)
  {
    entity.DateCreated = DateTime.UtcNow;
  }

  public void PrepForInsert(IList<T> entities)
  {
    foreach (var entity in entities)
    {
      PrepForInsert(entity);
    }
  }
}