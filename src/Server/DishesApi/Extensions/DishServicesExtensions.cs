using DishesApi.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DishesApi.Extensions;

public static class DishServicesExtensions
{
    extension(IApplicationBuilder builder)
    {
        public void RegisterDbContexts()
        {
            var serviceScopeFactory = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using var scope = serviceScopeFactory.CreateScope()
                              ?? throw new InvalidOperationException("Cannot access the service scope");

            var serviceProvider = scope.ServiceProvider
                                  ?? throw new InvalidOperationException("Cannot access the service provider.");

            var context = serviceProvider.GetRequiredService<DishesDbContext>();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
