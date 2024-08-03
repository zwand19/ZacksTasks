namespace Repositories;

using Microsoft.EntityFrameworkCore;

public class OriginDbContext : DbContext
{
  public DbSet<Task> Tasks { get; set; }
  
  public OriginDbContext(DbContextOptions<OriginDbContext> options) : base(options)
  {
  }
}