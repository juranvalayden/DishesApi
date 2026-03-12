using DishApi.Application.Handlers;
using DishesApi.EndpointFilters;

namespace DishesApi.Extensions;

public static class EndpointRouteBuilderExtensions
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public void RegisterDishesEndpoints()
        {
            var dishesEndpoints = endpointRouteBuilder
                .MapGroup("/dishes")
                .RequireAuthorization();

            dishesEndpoints
                .MapGet("", DishesHandlers.GetDishesAsync)
                .RequireAuthorization(); 

            dishesEndpoints
                .MapGet("/{dishName}", DishesHandlers.GetDishByNameAsync)
                .AllowAnonymous();

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

        public void RegisterIngredientsEndpoints()
        {
            var ingredientsEndpoints = endpointRouteBuilder
                .MapGroup("/dishes/{dishId:guid}/ingredients")
                .RequireAuthorization();

            ingredientsEndpoints
                .MapGet("", IngredientsHandlers.GetIngredientsAsync);

            ingredientsEndpoints
                .MapPost("", () =>
                {
                    throw new NotImplementedException();
                });
        }
    }
}