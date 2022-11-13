namespace ToDo_List.Models;

public class ToDoItemRequest
{
    public string Name { get; set; }
    public int Priority { get; set; }
    public bool Completed { get; set; }
}