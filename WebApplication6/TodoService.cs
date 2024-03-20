using WebApplication6.Model;

namespace WebApplication6
{
    public interface ITodoService
    {
        Todo? GetTodoById(int id);
        List<Todo> GetTodos();
        void DeleteTodoById(int id);
        Todo AddTodo(Todo todo);
    }

    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;
        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public Todo? GetTodoById(int id)
        {
            return _context.Todos.SingleOrDefault(x => x.Id == id);
        }

        public List<Todo> GetTodos()
        {
            return _context.Todos.ToList();
        }

        public void DeleteTodoById(int id)
        {
            var obj = _context.Todos.SingleOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return;
            }
            _context.Todos.Remove(obj);
            _context.SaveChanges();
        }

        public Todo AddTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return todo;
        }
    }
}
