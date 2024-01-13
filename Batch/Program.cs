using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Batch.Extensions;
using Batch.Services.Interface;
using Batch.Domain.Models;
using Batch.Services;

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

    private static async Task Run(IServiceProvider serviceProvider)
    {
        
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