using Azure;
using Azure.Data.Tables;

namespace ToDo_List.Infrastructure.ToDoService.Models;

public class ToDoTableRecord :  ITableEntity
{
    public string Name { get; set; }
    public int Priority { get; set; }
    public bool Completed { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

}