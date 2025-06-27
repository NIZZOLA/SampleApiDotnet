using Microsoft.AspNetCore.Mvc;
using Store.Application.Contracts;
using Store.Application.Interfaces;
namespace Store.Api.Endpoints;

public static class StoreEndpoints
{
    public static void MapStorePostRequestModelEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Store").WithTags(nameof(StorePostRequestModel));

        group.MapPost("/", async ([FromServices] IStoreAppService appService, [FromBody] StorePostRequestModel model) =>
        {
            var result = await appService.Save(model);
            if (result.IsFailed)
                return Results.BadRequest(result.Errors.ToList());

            return TypedResults.Created($"/api/StorePostRequestModels/{result.Value.Id}", result.Value);
        })
         .Produces<IList<FluentResults.IError>>(StatusCodes.Status400BadRequest)
         .Produces<StoreResponseModel>(StatusCodes.Status201Created)
         .WithName("CreateStorePost")
         .RequireRateLimiting(RateLimitOptions.MyRateLimit)
         .WithOpenApi();

        group.MapGet("/", async ([FromServices] IStoreAppService appService) =>
        {
            return await appService.GetAll();
        })
         .Produces<IList<StoreResponseModel>>(StatusCodes.Status200OK)
         .WithName("GetAllStores")
         .WithOpenApi();

        group.MapGet("/{id}", async ([FromServices] IStoreAppService appService, [FromQuery] Guid id) =>
        {
            var result = await appService.GetOne(id);
            
            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        })
         .Produces<StoreResponseModel>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound)
         .WithName("GetStoreById")
         .WithOpenApi();

        group.MapGet("/limited/{id}", async ([FromServices] IStoreAppService appService, [FromQuery] Guid id) =>
        {
            var result = await appService.GetOne(id);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        })
         .Produces<StoreResponseModel>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound)
         .WithName("GetStoreByIdLimited")
         .RequireRateLimiting(RateLimitOptions.MyRateLimit)
         .WithOpenApi();


        group.MapPut("/{id}", async ([FromServices] IStoreAppService appService, Guid id, StorePutRequestModel input) =>
        {
            var result = await appService.Update(id, input);
            if (result.IsFailed)
                return Results.BadRequest(result.Errors.ToList());

            return TypedResults.NoContent();
        })
         .Produces<IList<FluentResults.IError>>(StatusCodes.Status400BadRequest)
         .Produces(StatusCodes.Status204NoContent)
         .WithName("UpdateStorePut")
         .WithOpenApi();

        group.MapDelete("/{id}", async([FromServices] IStoreAppService appService,Guid id) =>
        {
            var result = await appService.Delete(id);
            return TypedResults.Ok(new StoreResponseModel { Id = id });
        })
         .Produces(StatusCodes.Status200OK)
         .WithName("DeleteStore")
         .WithOpenApi();
    }
}
