using DishesApi.Domain.Entities;
using DishesApi.Domain.Interfaces;
using DishesApi.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DishesApi.Infrastructure.Repositories;

public class DishRepository : IDishRepository
{
    private readonly DishesDbContext _dishesDbContext;

    public DishRepository(DishesDbContext dishesDbContext)
    {
        _dishesDbContext = dishesDbContext ?? throw new ArgumentNullException(nameof(dishesDbContext));
    }

    public async Task<IEnumerable<Dish>> GetDishesAsync(bool shouldIncludeIngredients = false)
    {
        if (shouldIncludeIngredients)
        {
            return await _dishesDbContext
                .Dishes
                .Include(d => d.Ingredients)
                .ToListAsync();
        }

        return await _dishesDbContext.Dishes.ToListAsync();
    }

    public async Task<Dish?> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients = false)
    {
        if (shouldIncludeIngredients)
        {
            return await _dishesDbContext
                .Dishes
                .Include(i => i.Ingredients)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        return await _dishesDbContext.Dishes.FirstOrDefaultAsync(d => d.Id == id);
    }

    public void AddDish(Dish entity)
    {
        _dishesDbContext.Dishes.Add(entity);
    }

    public void UpdateDish(Dish entity)
    {
        _dishesDbContext.Dishes.Update(entity);
    }

    public void DeleteDish(Dish entity)
    {
        _dishesDbContext.Dishes.Remove(entity);
    }

    public async Task<IEnumerable<Dish>> GetDishesByNameAsync(string name, bool shouldIncludeIngredients = false)
    {
        if (shouldIncludeIngredients)
        {
            return await _dishesDbContext
                .Dishes
                .Include(i => i.Ingredients)
                .Where(d => d.Name == name).ToListAsync();
        }

        return await _dishesDbContext
            .Dishes
            .Where(d => d.Name == name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ingredient>> GetIngredientsAsync(Guid dishId)
    {
        var dish = await _dishesDbContext
            .Dishes
            .Include(i => i.Ingredients)
            .FirstOrDefaultAsync(d => d.Id == dishId);

        return dish?.Ingredients ?? [];
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dishesDbContext.SaveChangesAsync() > 0;
    }
}
