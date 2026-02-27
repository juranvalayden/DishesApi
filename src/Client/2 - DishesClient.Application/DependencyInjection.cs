using DishesClient.Application.Helpers;
using DishesClient.Application.Interfaces;
using DishesClient.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DishesClient.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<JsonSerializerOptionsWrapper>();

        services
            .AddHttpClient(Constants.DishClient, httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:7043");
                httpClient.Timeout = new TimeSpan(0, 0, 0, 30, 0);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new SocketsHttpHandler();
                handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
                return handler;
            });

        services.TryAddScoped<IIntegrationService, DishesService>();
    }
}
