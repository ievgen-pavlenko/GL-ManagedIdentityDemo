using Azure.Data.Tables;
using ToDo_List.Infrastructure.ToDoService.Models;
using ToDo_List.Models;

namespace ToDo_List.Infrastructure.ToDoService;

public class ToDoService : IToDoService
{
    private readonly string _tableName;
    private readonly TableClient _client;
    public ToDoService(string tableName, TableClient client)
    {
        _tableName = tableName;
        _client = client;
    }

    public async Task<ToDoItemResponse> UpsertItemAsync(ToDoItemRequest toDoRequest)
    {
        var id = Guid.NewGuid();
        var response = await _client.UpsertEntityAsync(new ToDoTableRecord
        {
            PartitionKey = _tableName,
            RowKey = id.ToString(),
            Name = toDoRequest.Name,
            Priority = toDoRequest.Priority,
            Completed = toDoRequest.Completed,
        });

        if (response.IsError)
        {
            throw new ToDoServiceException(response.ReasonPhrase);
        }

        return new ToDoItemResponse
        {
            Id = id,
            Name = toDoRequest.Name,
            Priority = toDoRequest.Priority,
            Completed = toDoRequest.Completed
        };
    }

    public async Task<ToDoItemResponse?> GetItemAsync(Guid id)
    {
        var response = await _client.GetEntityIfExistsAsync<ToDoTableRecord>(_tableName, id.ToString());
        return response.HasValue ? response.Value.MapAsToDoItemResponse() : null;
    }

    public async Task<ToDoItemResponse?> UpdateItemAsync(Guid id, ToDoItemRequest toDoRequest)
    {
        var response = await _client.GetEntityAsync<ToDoTableRecord>(_tableName, id.ToString());
        if (!response.HasValue)
        {
            return null;
        }

        var doTableRecord = response.Value;
        doTableRecord.Name = toDoRequest.Name;
        doTableRecord.Priority = toDoRequest.Priority;
        doTableRecord.Completed = toDoRequest.Completed;

        await _client.UpdateEntityAsync(doTableRecord, response.Value.ETag);

        response = await _client.GetEntityAsync<ToDoTableRecord>(_tableName, id.ToString());

        return response.Value.MapAsToDoItemResponse();
    }

    public Task DeleteItemAsync(Guid id)
    {
        return _client.DeleteEntityAsync(_tableName, id.ToString());
    }

    public async Task<IEnumerable<ToDoItemResponse>> AllItemsAsync()
    {
        var result = new List<ToDoItemResponse>();
        var queryAsync = _client.QueryAsync<ToDoTableRecord>();
        await foreach (var page in queryAsync.AsPages())
        {
            result.AddRange(page.Values.Select(i => i.MapAsToDoItemResponse()));
        }

        return result;
    }
}