using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEF.Context;

var builder = WebApplication.CreateBuilder(args);

// Create the configuration to add an in-memory DB
/* builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("TaskDB")); */

// Create the configuration to add a SQL database connection
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("cnTasks"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Create an endpoint to see whether the DB is being created
app.MapGet("/dbconnection", (TasksContext dbContext) =>
{
  dbContext.Database.EnsureCreated();
  return Results.Ok("Data base in memory: " + dbContext.Database.IsInMemory());
});

// GET
app.MapGet("/api/tasks", async ([FromServices] TasksContext dbContext) =>
{
  var tasks = await dbContext.Tasks.Include(tasks => tasks.Category).ToListAsync();
  return Results.Ok(tasks);
});

// POST
app.MapPost("/api/tasks", async ([FromServices] TasksContext dbContext, [FromBody] ProjectEF.Models.Task task) =>
{
  task.TaskID = Guid.NewGuid();
  task.CreationDate = DateTime.Now;

  await dbContext.Tasks.AddAsync(task);
  await dbContext.SaveChangesAsync();

  return Results.Ok(task);
});

// UPDATE
app.MapPut("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromBody] ProjectEF.Models.Task task, [FromRoute] Guid id) =>
{
  var prevTask = await dbContext.Tasks.FindAsync(id);
  if (prevTask == null)
  {
    return Results.NotFound();
  }

  prevTask.Title = task.Title;
  prevTask.Description = task.Description;
  prevTask.TaskPriority = task.TaskPriority;
  prevTask.Summary = task.Summary;
  prevTask.CategoryID = task.CategoryID;

  await dbContext.SaveChangesAsync();

  return Results.Ok(task);
});

// DELETE
app.MapDelete("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id) =>
{
  var task = await dbContext.Tasks.FindAsync(id);
  if (task == null)
  {
    return Results.NotFound();
  }
  dbContext.Tasks.Remove(task);
  await dbContext.SaveChangesAsync();
  return Results.Ok(task);
});

app.Run();
