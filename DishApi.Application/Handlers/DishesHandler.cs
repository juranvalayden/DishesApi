using AutoMapper;
using DishApi.Application.Dtos.Dishes;
using DishApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace DishApi.Application.Handlers;

public static class DishesHandlers
{
    public static async Task<Ok<IEnumerable<DishDto>>> GetDishesAsync(IDishService dishService, ClaimsPrincipal claimsPrincipal, IMapper mapper, string? name)
    {
        Console.WriteLine($"User authenticated? {claimsPrincipal.Identity?.IsAuthenticated}");

        if (string.IsNullOrWhiteSpace(name))
        {
            var dishes = await dishService.GetDishesAsync(true);
            return TypedResults.Ok(mapper.Map<IEnumerable<DishDto>>(dishes));
        }

        var dishesFilteredByName = await dishService.GetDishesByNameAsync(name);

        return TypedResults.Ok(dishesFilteredByName);
    }

    public static async Task<Results<NotFound, Ok<DishDto>>> GetDishByIdAsync(IDishService dishService, IMapper mapper, Guid dishId)
    {
        var dishEntity = await dishService.GetDishByIdAsync(dishId);

        if (dishEntity == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(dishEntity);
    }

    public static async Task<Ok<DishDto>> GetDishByNameAsync(IDishService dishService, IMapper mapper, string dishName)
    {
        var dish = await dishService.GetDishesByNameAsync(dishName);
        return TypedResults.Ok(dish.FirstOrDefault());
    }

    public static async Task<Results<CreatedAtRoute<DishDto>, BadRequest>> CreateDishAsync(IDishService dishService, IMapper mapper, DishForCreationDto dishForCreationDto)
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

    public static async Task<Results<NotFound, NoContent, BadRequest>> UpdateDishAsync(IDishService dishService, IMapper mapper, Guid dishId, DishForUpdateDto dishForUpdateDto)
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