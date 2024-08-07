﻿using Models;

namespace Repositories;

public interface IRepositoryUtility<T> where T : BaseEntity
{
  void PrepForInsert(T entity);
  
  void PrepForInsert(IList<T> entities);
}