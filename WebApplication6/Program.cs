using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using WebApplication6;
using WebApplication6.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseSqlite("Data Source=TodoDb.db");
});


var app = builder.Build();

app.UseRewriter(new RewriteOptions().AddRedirect("modos/(.*)", "todos/$1"));

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Path} {DateTime.Now}");
    await next();
    Console.WriteLine($"Response: {context.Response.StatusCode} {DateTime.Now}");
});

var myTodos = new List<Todo>();

app.MapGet("/todos", (ITodoService todoService) =>
{
    return todoService.GetTodos();
});

app.MapPost("/todos", (Todo todo, ITodoService todoService) =>
{
    var model = todoService.AddTodo(todo);
    return Results.Ok(model);
}).AddEndpointFilter(async (context, next) =>
{
    var taskArgument = context.GetArgument<Todo>(0);
    var errors = new Dictionary<string, string[]>();

    if (taskArgument.Date < DateTime.Now)
        errors.Add("Date", ["The Date field must be a date in the future."]);

    if (errors.Count > 0)
        return Results.ValidationProblem(errors); // 400
    return await next(context);
});

// detail service
app.MapGet("/todos/{id}", (int id, ITodoService todoService) =>
{
    var detail = todoService.GetTodoById(id);
    if (detail == null)
    {
        return Results.NotFound("obje bulunamadi");
    }
    return Results.Ok(detail);
});

// delete service
app.MapDelete("/todos/{id:int}", (int id, ITodoService todoService) =>
{
    var model = todoService.GetTodoById(id);
    if (model == null)
    {
        return Results.NotFound("obje bulunamadi");
    }
    todoService.DeleteTodoById(id);
    return Results.NoContent();
});

app.Run();

