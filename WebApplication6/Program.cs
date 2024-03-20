var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var myTodos = new List<Todo>();

app.MapGet("/", () => "hello world");

app.MapGet("/todos", () => myTodos);

app.MapPost("/todos", (Todo todo) =>
{
    myTodos.Add(todo);
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapGet("/todos/{id}", (int id) => myTodos.SingleOrDefault(x => x.Id == id));

app.MapDelete("/todos/{id:int}", (int id) =>
{
    var obj = myTodos.SingleOrDefault(x => x.Id == id);
    if (obj == null)
    {
        return Results.NotFound("obje bulunamadi");
    }
    myTodos.Remove(obj);
    return Results.Ok("obje silindi");
});

app.Run();


public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
}