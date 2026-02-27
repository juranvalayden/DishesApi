using DishesApi.Domain.Entities;

namespace DishesApi.Domain.Interfaces;

public interface IDishRepository
{
    Task<IEnumerable<Dish>> GetDishesAsync(bool shouldIncludeIngredients = false);
    Task<IEnumerable<Dish>> GetDishesByNameAsync(string name, bool shouldIncludeIngredients = false);
    Task<Dish?> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients = false);

    void AddDish(Dish entity);
    void UpdateDish(Dish entity);
    void DeleteDish(Dish entity);

    Task<IEnumerable<Ingredient>> GetIngredientsAsync(Guid dishId);

    Task<bool> SaveChangesAsync();
}
