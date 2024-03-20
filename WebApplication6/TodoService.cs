using WebApplication6.Model;

namespace WebApplication6;

public interface ITodoService
{
    Todo? GetTodoById(int id);
    List<Todo> GetTodos();
    void DeleteTodoById(int id);
    Todo AddTodo(Todo todo);
}

public class TodoService : ITodoService
{
    private List<Todo> _todos =
    [
        new Todo { Id = 1, Date = DateTime.Now, Title = "Todo 1", MyDescription = "Description 1" },
        new Todo { Id = 2, Date = DateTime.Now, Title = "Todo 2", MyDescription = "Description 2" },
        new Todo { Id = 3, Date = DateTime.Now, Title = "Todo 3", MyDescription = "Description 3" },
    ];

    public Todo AddTodo(Todo todo)
    {
        todo.Id = _todos.Max(t => t.Id) + 1;
        _todos.Add(todo);
        return todo;
    }

    public void DeleteTodoById(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
        {
            _todos.Remove(todo);
        }
    }

    public Todo? GetTodoById(int id)
    {
        return _todos.FirstOrDefault(t => t.Id == id);
    }

    public List<Todo> GetTodos()
    {
        return _todos;
    }
}
