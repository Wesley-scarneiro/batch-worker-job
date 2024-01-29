using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Batch.Extensions;
using Batch.Application.Jobs;
using Batch.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Batch.Application.Notifications;

namespace Batch;
class Program
{
    /*
        Initializes application settings
    */
    private static IConfiguration BuildConfiguration()
    {
        var environment = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")) ? "Development" : Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"Configurations/appsettings-{environment}.json", optional: true, reloadOnChange: true)
            .Build();
        return configuration;
    }

    /*
        Dependency injection settings
    */
    private static IServiceProvider DependencyInjection(IConfiguration configuration)
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .AddRepository(configuration)
            .AddServices(configuration)
            .AddAplication()
            .BuildServiceProvider();
        return serviceProvider;
    }

    private static void ExitCode(IWorker worker, ILogger<Program> logger)
    {
        if (worker.HasNotification)
        {
            logger.LogInformation("Program ended with one or more notifications");
            foreach (var notification in worker.Notifications)
            {
                if (notification.Level == NotificationLevel.WARNING) logger.LogWarning("{NOTIFICATION}", notification.ToString());
                else logger.LogError("{NOTIFICATION}", notification.ToString());
            }
            Environment.Exit(1);
        }
        logger.LogInformation("Program completed without any complications");
    }

    /*
        Worker queue settings and job definitions
    */
    private static async Task Run(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Starting program execution");
        var worker = serviceProvider.GetRequiredService<IWorker>()
            .CreateJob<JobCreateSupplier>()
            .CreateJob<JobCreateProduct>()
            .CreateJob<JobUpdateProduct>()
            .CreateJob<JobDeleteProduct>()
            .CreateJob<JobViewProduct>();
        await worker.Run();
        ExitCode(worker, logger);
    }

    public static async Task Main()
    {
        try
        {
            var configuration = BuildConfiguration();
            var serviceProvider = DependencyInjection(configuration);
            await Run(serviceProvider);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}