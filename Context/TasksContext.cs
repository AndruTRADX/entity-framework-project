using Microsoft.EntityFrameworkCore;
using ProjectEF.Models;
using Task = ProjectEF.Models.Task;

namespace ProjectEF.Context;

public class TasksContext(DbContextOptions<TasksContext> options) : DbContext(options)
{
  public DbSet<Category> Categories { get; set; }
  public DbSet<Task> Tasks { get; set; }

  // Whe overriding a method, you should always use "protected"
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Fluent API
    modelBuilder.Entity<Category>(category =>
    {
      category.ToTable("Category");
      category.HasKey(category => category.CategoryID);
      category.Property(category => category.Name).IsRequired().HasMaxLength(150);
      category.Property(category => category.Description).IsRequired().HasMaxLength(1000);
    });

    modelBuilder.Entity<Task>(task =>
    {
      task.ToTable("Task");
      task.HasKey(task => task.TaskID);
      /* Expressing that category has many tasks associated with it */
      task.HasOne(category => category.Category).WithMany(task => task.Tasks).HasForeignKey(category => category.CategoryID);
      task.Property(task => task.Title).IsRequired().HasMaxLength(200);
      task.Property(task => task.Description).IsRequired().HasMaxLength(1000);
      task.Ignore(task => task.Summary); // Not mapped
    });
  }
}