using DishApi.Application.Dtos.Ingredients;
using DishApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DishApi.Application.Handlers;

public static class IngredientsHandlers
{
    public static async Task<Results<NotFound, Ok<IEnumerable<IngredientDto>>>> GetIngredientsAsync(IDishService dishService, Guid dishId)
    {
        var dishDto = await dishService.GetDishByIdAsync(dishId, true);

        if (dishDto == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(dishDto.Ingredients);
    }
}