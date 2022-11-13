using Azure.Data.Tables;

namespace ToDo_List.Infrastructure.TableClientFactory;

public interface ITableClientFactory
{
    TableClient GetTableClient(string tableName);
}