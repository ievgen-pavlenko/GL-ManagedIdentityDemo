using Azure.Data.Tables;
using Azure.Identity;

namespace ToDo_List.Infrastructure.TableClientFactory;

public class TableClientFactory : ITableClientFactory
{
    private readonly string _accountName;
    public TableClientFactory(string accountName)
    {
        _accountName = accountName;
    }

    public TableClient GetTableClient(string tableName)
    {
        var tableClient = new TableClient(
            new Uri($"https://{_accountName}.table.core.windows.net/"),
            tableName,
            new DefaultAzureCredential(new DefaultAzureCredentialOptions()
            {
                TenantId = "5c3d2a54-2e29-46a4-863f-a97e16453d71"
            }));
        tableClient.CreateIfNotExists();
        return tableClient;
    }
}