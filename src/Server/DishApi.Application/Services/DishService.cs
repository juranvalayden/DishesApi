using DishApi.Application.Dtos.Dishes;
using DishApi.Application.Dtos.Ingredients;
using DishApi.Application.Interfaces;
using DishApi.Application.Mappers;
using DishesApi.Domain.Interfaces;

namespace DishApi.Application.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
    }

    public async Task<IEnumerable<DishDto>> GetDishesAsync(bool shouldIncludeIngredients = false)
    {
        var dishes = await _dishRepository.GetDishesAsync(shouldIncludeIngredients);

        return DishesMapper.MapDtos(dishes);
    }

    public async Task<IEnumerable<DishDto>> GetDishesByNameAsync(string name, bool shouldIncludeIngredients = false)
    {
        var dishes = await _dishRepository.GetDishesByNameAsync(name, shouldIncludeIngredients);

        return DishesMapper.MapDtos(dishes);
    }

    public async Task<DishDto?> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients = false)
    {
        var dish = await _dishRepository.GetDishByIdAsync(id, shouldIncludeIngredients);

        return dish != null
            ? DishesMapper.MapDto(dish)
            : null;
    }

    public async Task<DishDto?> AddDishAsync(DishForCreationDto dishForCreationDto)
    {
        var dish = DishesMapper.MapEntity(dishForCreationDto);

        _dishRepository.AddDish(dish);

        var hasSaved = await _dishRepository.SaveChangesAsync();

        return hasSaved 
            ? DishesMapper.MapDto(dish)
            : null;
    }

    public async Task<DishDto?> UpdateDishAsync(Guid id, DishForUpdateDto dishForUpdateDto)
    {
        var existingDish = await _dishRepository.GetDishByIdAsync(id);

        if (existingDish == null) return null;

        var dish = DishesMapper.MapEntity(dishForUpdateDto, existingDish);
        
        _dishRepository.UpdateDish(dish);

        var hasSaved =  await _dishRepository.SaveChangesAsync();

        return hasSaved
            ? DishesMapper.MapDto(dish)
            : null;
    }

    public async Task<bool> DeleteDishAsync(Guid id)
    {
        var existingDish = await _dishRepository.GetDishByIdAsync(id);

        if (existingDish == null) return false;

        _dishRepository.DeleteDish(existingDish);

        return await _dishRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<IngredientDto>> GetIngredientsAsync(Guid dishId)
    {
        var ingredients = await _dishRepository.GetIngredientsAsync(dishId);
        return DishesMapper.MapEntities(ingredients);
    }
}