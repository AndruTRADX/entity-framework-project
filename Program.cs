using ProjectEF.Context;
using Microsoft.EntityFrameworkCore;

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

app.Run();
