using System.Runtime.CompilerServices;
using ToDo_List;
using ToDo_List.Infrastructure.TableClientFactory;
using ToDo_List.Infrastructure.ToDoService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(typeof(ITableClientFactory), provider =>
{
    var config = provider.GetService<IConfiguration>();
    var storageAccountName = config[ConfigPropertiesNames.StorageAccountName];
    return new TableClientFactory(storageAccountName);
});

builder.Services.AddScoped(typeof(IToDoService), provider =>
{
    var config = provider.GetService<IConfiguration>();
    var tableClientFactory = provider.GetService<ITableClientFactory>();
    var tableName = config[ConfigPropertiesNames.TableName] ?? "todos";
    return new ToDoService(tableName, tableClientFactory!.GetTableClient(tableName));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.RegisterToDoEndpoints();

app.Run();