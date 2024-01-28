using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Batch.Extensions;
using Batch.Application;
using Batch.Application.Jobs;
using Batch.Application.Interfaces;

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
            .AddRepository(configuration)
            .AddServices(configuration)
            .AddAplication()
            .BuildServiceProvider();
        return serviceProvider;
    }

    /*
        Worker queue settings and job definitions
    */
    private static async Task Run(IServiceProvider serviceProvider)
    {
        var worker = serviceProvider.GetRequiredService<IWorker>()
            .CreateJob<CreateProduct>()
            .CreateJob<UpdateProduct>();
        await worker.Run();
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