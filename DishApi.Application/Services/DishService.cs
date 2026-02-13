using AutoMapper;
using DishApi.Application.Dtos.Dishes;
using DishApi.Application.Dtos.Ingredients;
using DishApi.Application.Interfaces;
using DishesApi.Domain.Entities;
using DishesApi.Domain.Interfaces;

namespace DishApi.Application.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;

    public DishService(IDishRepository dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<DishDto>> GetDishesAsync(bool shouldIncludeIngredients = false)
    {
        var dishes = await _dishRepository.GetDishesAsync(shouldIncludeIngredients);
        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

    public async Task<IEnumerable<DishDto>> GetDishesByNameAsync(string name, bool shouldIncludeIngredients = false)
    {
        var dishes = await _dishRepository.GetDishesByNameAsync(name, shouldIncludeIngredients);
        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

    public async Task<DishDto?> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients = false)
    {
        var dish = await _dishRepository.GetDishByIdAsync(id, shouldIncludeIngredients);

        return dish != null
            ? _mapper.Map<DishDto>(dish)
            : null;
    }

    public async Task<DishDto?> AddDishAsync(DishForCreationDto dishForCreationDto)
    {
        var dish = _mapper.Map<Dish>(dishForCreationDto);

        _dishRepository.AddDish(dish);

        var hasSaved = await _dishRepository.SaveChangesAsync();

        return hasSaved 
            ? _mapper.Map<DishDto>(dish) 
            : null;
    }

    public async Task<DishDto?> UpdateDishAsync(Guid id, DishForUpdateDto dishForUpdateDto)
    {
        var existingDish = await _dishRepository.GetDishByIdAsync(id);

        if (existingDish == null) return null;

        var dish = _mapper.Map(dishForUpdateDto, existingDish);

        _dishRepository.UpdateDish(dish);

        var hasSaved =  await _dishRepository.SaveChangesAsync();

        return hasSaved
            ? _mapper.Map<DishDto>(dish)
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
        return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
    }
}