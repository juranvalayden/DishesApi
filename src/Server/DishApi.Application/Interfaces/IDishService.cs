using DishApi.Application.Dtos.Dishes;
using DishApi.Application.Dtos.Ingredients;

namespace DishApi.Application.Interfaces;

public interface IDishService 
{
    Task<IEnumerable<DishDto>> GetDishesAsync(bool shouldIncludeIngredients = false);
    Task<IEnumerable<DishDto>> GetDishesByNameAsync(string name, bool shouldIncludeIngredients = false);
    Task<DishDto?> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients = false);

    Task<DishDto?> AddDishAsync(DishForCreationDto dishForCreationDto);
    Task<DishDto?> UpdateDishAsync(Guid id, DishForUpdateDto dishForUpdateDto);
    Task<bool> DeleteDishAsync(Guid id);

    Task<IEnumerable<IngredientDto>> GetIngredientsAsync(Guid dishId);
}