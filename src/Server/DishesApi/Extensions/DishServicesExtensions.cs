using DishesApi.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DishesApi.Extensions;

public static class DishServicesExtensions
{
    public static void RegisterDbContexts(this IApplicationBuilder builder)
    {
        var serviceScopeFactory = builder
            .ApplicationServices
            .GetRequiredService<IServiceScopeFactory>();

        using var scopeFactory = serviceScopeFactory?.CreateScope() 
                                 ?? throw new InvalidOperationException("Cannot access the service scope");

        var provider = scopeFactory.ServiceProvider
                       ?? throw new InvalidOperationException("Cannot access the service provider.");

        var context = provider.GetRequiredService<DishesDbContext>();
        context.Database.EnsureDeleted();
        context.Database.Migrate();
    }
}
