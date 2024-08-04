using Models;

namespace Repositories;

using Microsoft.EntityFrameworkCore;

public class ZackTasksDbContext : DbContext
{
  public DbSet<ZackTask> Tasks { get; set; }
  
  public ZackTasksDbContext(DbContextOptions<ZackTasksDbContext> options) : base(options)
  {
  }
}