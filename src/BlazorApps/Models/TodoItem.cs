using System;

namespace BlazorApps.Models;

public class TodoItem
{
    public string? Title { get; set; }
    public bool IsDone { get; set; } = false;
}
