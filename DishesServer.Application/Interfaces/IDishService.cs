using DishesServer.Application.Dtos.Dishes;

namespace DishesServer.Application.Interfaces;

public interface IDishService : IIntegrationService
{
    Task<ICollection<DishDto>> GetDishesAsync(CancellationToken cancellationToken = default);
}
