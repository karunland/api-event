using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Model;
public class Todo
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public DateTime Date { get; set; }
    [Required]
    public string? MyDescription { get; internal set; }
}
