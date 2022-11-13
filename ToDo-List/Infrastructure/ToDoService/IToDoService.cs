using ToDo_List.Models;

namespace ToDo_List.Infrastructure.ToDoService;

public interface IToDoService
{
    Task<ToDoItemResponse> UpsertItemAsync(ToDoItemRequest toDoRequest);
    Task<ToDoItemResponse?> GetItemAsync(Guid id);
    Task<ToDoItemResponse?> UpdateItemAsync(Guid id, ToDoItemRequest toDoRequest);
    Task DeleteItemAsync(Guid id);
    Task<IEnumerable<ToDoItemResponse>> AllItemsAsync();
}