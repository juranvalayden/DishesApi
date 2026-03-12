using DishApi.Application.Dtos.Dishes;
using DishApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace DishApi.Application.Handlers;

public static class DishesHandlers
{
    public static async Task<Ok<IEnumerable<DishDto>>> GetDishesAsync(IDishService dishService, ClaimsPrincipal claimsPrincipal, string? name)
    {
        Console.WriteLine($"User authenticated? {claimsPrincipal.Identity?.IsAuthenticated}");

        if (string.IsNullOrWhiteSpace(name))
        {
            var dishes = await dishService.GetDishesAsync(true);

            return TypedResults.Ok(dishes);
        }

        var dishesFilteredByName = await dishService.GetDishesByNameAsync(name);

        return TypedResults.Ok(dishesFilteredByName);
    }

    public static async Task<Results<NotFound, Ok<DishDto>>> GetDishByIdAsync(IDishService dishService, Guid dishId)
    {
        var dishDto = await dishService.GetDishByIdAsync(dishId);

        if (dishDto == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(dishDto);
    }

    public static async Task<Ok<DishDto>> GetDishByNameAsync(IDishService dishService, string dishName)
    {
        var dishDtos = await dishService.GetDishesByNameAsync(dishName);

        return TypedResults.Ok(dishDtos.FirstOrDefault());
    }

    public static async Task<Results<CreatedAtRoute<DishDto>, BadRequest>> CreateDishAsync(IDishService dishService, DishForCreationDto dishForCreationDto)
    {
        var dishToReturn = await dishService.AddDishAsync(dishForCreationDto);

        if (dishToReturn == null) return TypedResults.BadRequest();

        return TypedResults.CreatedAtRoute(
            dishToReturn,
            "GetDish",
            new
            {
                dishId = dishToReturn.Id
            });
    }

    public static async Task<Results<NotFound, NoContent, BadRequest>> UpdateDishAsync(IDishService dishService, Guid dishId, DishForUpdateDto dishForUpdateDto)
    {
        var dishToReturn = await dishService.UpdateDishAsync(dishId, dishForUpdateDto);

        if (dishToReturn == null) return TypedResults.BadRequest();

        return TypedResults.NoContent();
    }

    public static async Task<Results<NotFound, NoContent>> DeleteDishAsync(IDishService dishService, Guid dishId)
    {
        var hasDeleted = await dishService.DeleteDishAsync(dishId);

        if (hasDeleted) return TypedResults.NoContent();

        return TypedResults.NotFound();
    }
}