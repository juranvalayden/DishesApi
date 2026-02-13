using DishApi.Application.Interfaces;
using DishApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DishApi.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        var assembliesToScan = AppDomain.CurrentDomain.GetAssemblies();

        serviceCollection.AddAutoMapper(cfg => { cfg.AddMaps(assembliesToScan); });

        serviceCollection.AddScoped<IDishService, DishService>();
    }
}