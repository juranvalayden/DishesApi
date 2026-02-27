using DishesClient.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DishesClient.Presentation.Extensions;

public static class ServicesConfiguration
{
    public static async Task ConfigureServicesAsync(this IHost host)
    {
        var logger = host.Services.GetRequiredService<ILogger<Program>>() ?? throw new InvalidOperationException("No logger service service found.");

        try
        {
            logger.LogInformation("Host Created.");
            await host.Services.GetRequiredService<IIntegrationService>().RunAsync();
        }
        catch (Exception generalException)
        {
            logger.LogError(generalException, "An exception happened while running the integration service.");
        }
    }
}
