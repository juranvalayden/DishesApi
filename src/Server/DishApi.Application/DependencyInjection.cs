using DishApi.Application.Interfaces;
using DishApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DishApi.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDishService, DishService>();
    }
}