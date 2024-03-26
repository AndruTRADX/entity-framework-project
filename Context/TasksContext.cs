using Microsoft.EntityFrameworkCore;
using ProjectEF.Models;

namespace ProjectEF.Context;

public class TasksContext(DbContextOptions<TasksContext> options) : DbContext(options)
{
  public DbSet<Category> Categories { get; set; }
  public DbSet<Models.Task> Tasks { get; set; }
}