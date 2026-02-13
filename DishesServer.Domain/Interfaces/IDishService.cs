using DishApi.Application.Dtos.Dishes;

namespace DishesServer.Domain.Interfaces;

public interface IDishService
{
    Task<IEnumerable<DishDto>> GetDishesAsync(CancellationToken cancellationToken = default);
}
