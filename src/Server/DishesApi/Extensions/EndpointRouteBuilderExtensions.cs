using DishApi.Application.Handlers;
using DishesApi.EndpointFilters;

namespace DishesApi.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterDishesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var dishesEndpoints = endpointRouteBuilder.MapGroup("/dishes");

        dishesEndpoints
            .MapGet("", DishesHandlers.GetDishesAsync);

        dishesEndpoints
            .MapGet("/{dishName}", DishesHandlers.GetDishByNameAsync);

        dishesEndpoints
            .MapPost("", DishesHandlers.CreateDishAsync)
            .AddEndpointFilter<ValidateAnnotationsFilter>();

        var dishWithGuidIdEndpoints = dishesEndpoints.MapGroup("/{dishId:guid}");

        dishWithGuidIdEndpoints
            .MapGet("", DishesHandlers.GetDishByIdAsync)
            .WithName("GetDish");

        dishWithGuidIdEndpoints
            .MapPut("", DishesHandlers.UpdateDishAsync)
            .AddEndpointFilter<DishIsLockedFilter>();
        
        dishWithGuidIdEndpoints
            .MapDelete("", DishesHandlers.DeleteDishAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>();
    }

    public static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var ingredientsEndpoints = endpointRouteBuilder
            .MapGroup("/dishes/{dishId:guid}/ingredients");

        ingredientsEndpoints
            .MapGet("", IngredientsHandlers.GetIngredientsAsync);

        ingredientsEndpoints
            .MapPost("", () =>
        {
            throw new NotImplementedException();
        });
    }
}