using Microsoft.AspNetCore.Mvc;
using Store.Application.Contracts;
using Store.Application.Interfaces;
namespace Store.Api.Endpoints;

public static class StorePostRequestModelEndpoints
{
    public static void MapStorePostRequestModelEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Store").WithTags(nameof(StorePostRequestModel));

        group.MapGet("/", async ([FromServices] IStoreAppService appService) =>
        {
            return await appService.GetAll();
        })
        .WithName("GetAllStores")
        .WithOpenApi();

        group.MapGet("/{id}", async ([FromServices] IStoreAppService appService, [FromQuery] Guid id) =>
        {
            var result = await appService.GetOne(id);
            
            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        })
        .WithName("GetStoreById")
        .WithOpenApi();

        group.MapPut("/{id}", async ([FromServices] IStoreAppService appService, Guid id, StorePutRequestModel input) =>
        {
            var result = await appService.Update(id, input);
            if (result.IsFailed)
                return Results.BadRequest(result.Errors.ToList());

            return TypedResults.NoContent();
        })
        .WithName("UpdateStorePut")
        .WithOpenApi();

        group.MapPost("/", async ([FromServices] IStoreAppService appService, [FromBody] StorePostRequestModel model) =>
        {
            var result = await appService.Save(model);
            if (result.IsFailed)
                return Results.BadRequest(result.Errors.ToList());

            return TypedResults.Created($"/api/StorePostRequestModels/{result.Value.Id}", result.Value);
        })
        .WithName("CreateStorePost")
        .WithOpenApi();

        group.MapDelete("/{id}", async([FromServices] IStoreAppService appService,Guid id) =>
        {
            var result = await appService.Delete(id);
            return TypedResults.Ok(new StoreResponseModel { Id = id });
        })
        .WithName("DeleteStore")
        .WithOpenApi();
    }
}
