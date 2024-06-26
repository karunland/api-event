﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Model;
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
    [Required]
    public string? MyDescription { get; internal set; }
}
