using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDo_List.Infrastructure.ToDoService;
using ToDo_List.Models;

namespace ToDo_List;

public static class ToDoEndpointExtension
{
    public static void RegisterToDoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("todo");

        group.MapGet("/",
            (IToDoService toDoService) => toDoService.AllItemsAsync());

        group.MapGet("/{id:guid}", 
            async Task<Results<NotFound, Ok<ToDoItemResponse>>> (IToDoService toDoService, Guid id) =>
        {
            var result = await toDoService.GetItemAsync(id);
            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
        });

        group.MapPost("/",
            async Task<Ok<ToDoItemResponse>> ([FromBody] ToDoItemRequest todo, IToDoService toDoService) => 
                TypedResults.Ok(await toDoService.UpsertItemAsync(todo)));

        group.MapPatch("/{id:guid}",
            async Task<Results<NotFound, Ok<ToDoItemResponse>>> (IToDoService toDoService, Guid id, [FromBody] ToDoItemRequest value) =>
            {
                var result = await toDoService.UpdateItemAsync(id, value);
                return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
            });

        group.MapDelete("/{id:guid}",
            (IToDoService toDoService, Guid id) =>
                toDoService.DeleteItemAsync(id));
    }
}