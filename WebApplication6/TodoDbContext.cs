using Microsoft.EntityFrameworkCore;
using WebApplication6.Model;

namespace WebApplication6;

// suan proje db kullanmiyor
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }
}
