using DishesApi.Domain.Interfaces;
using DishesApi.Infrastructure.DbContexts;
using DishesApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DishesApi.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<DishesDbContext>(o =>
        {
            var connectionString = configuration["ConnectionStrings:DishesDBConnectionString"];
            o.UseSqlite(connectionString);
        });

        serviceCollection.AddTransient<IDishRepository, DishRepository>();
    }
}