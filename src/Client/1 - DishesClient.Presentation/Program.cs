using DishesClient.Application;
using DishesClient.Presentation.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddLogging(configure => configure.AddConsole());
        services.AddApplication();
    }).Build();

await host.ConfigureServicesAsync();

await host.RunAsync();