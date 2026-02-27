using DishesClient.Application.Dtos.Dishes;

namespace DishesClient.Application.Interfaces;

public interface IDishService : IIntegrationService
{
    Task<IEnumerable<DishDto>> GetDishesAsync(CancellationToken cancellationToken = default);
}
