using ToDo_List.Infrastructure.ToDoService.Models;
using ToDo_List.Models;

namespace ToDo_List.Infrastructure;

public static class Extensions
{
    public static ToDoItemResponse MapAsToDoItemResponse(this ToDoTableRecord tableRecord)
    {
        return new ToDoItemResponse
        {
            Id = Guid.Parse(tableRecord.RowKey),
            Name = tableRecord.Name,
            Priority = tableRecord.Priority,
            Completed = tableRecord.Completed,
        };
    }
}