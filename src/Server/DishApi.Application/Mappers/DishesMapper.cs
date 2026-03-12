using DishApi.Application.Dtos.Dishes;
using DishApi.Application.Dtos.Ingredients;
using DishesApi.Domain.Entities;

namespace DishApi.Application.Mappers;

public static class DishesMapper
{
    public static IEnumerable<DishDto> MapDtos(IEnumerable<Dish> dishes)
    {
        return dishes
            .Select(dish => new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
            })
            .ToList();
    }

    public static DishDto MapDto(Dish dish)
    {
        return new DishDto
        {
            Id = dish.Id,
            Name = dish.Name,
        };
    }

    public static Dish MapEntity(DishForCreationDto dishForCreationDto)
    {
        return new Dish
        {
            Name = dishForCreationDto.Name
        };
    }

    public static Dish MapEntity(DishForUpdateDto dishForUpdateDto, Dish existingDish)
    {
        existingDish.Name = dishForUpdateDto.Name;
        return existingDish;
    }

    public static IEnumerable<Dish> MapEntities(IEnumerable<DishDto> dtos)
    {
        return dtos
            .Select(dish => new Dish
            {
                Id = dish.Id,
                Name = dish.Name,
            })
            .ToList();
    }

    public static IEnumerable<IngredientDto> MapEntities(IEnumerable<Ingredient> ingredients)
    {
        return ingredients.Select(i => new IngredientDto
        {
            Id = i.Id,
            Name  = i.Name
        }).ToList();
    }
}
