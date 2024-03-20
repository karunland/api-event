using Microsoft.EntityFrameworkCore;
using WebApplication6.Model;

namespace WebApplication6
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
