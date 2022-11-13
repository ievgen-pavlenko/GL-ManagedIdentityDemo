namespace ToDo_List.Infrastructure.ToDoService;

public class ToDoServiceException : Exception
{
    public ToDoServiceException(string message): base(message)
    {
    }
}