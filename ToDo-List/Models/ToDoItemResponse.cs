namespace ToDo_List.Models;

public class ToDoItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public bool Completed { get; set; }
}