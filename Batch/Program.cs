using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Batch.Extensions;
using Batch.Repository;
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
            .BuildServiceProvider();
        return serviceProvider;
    }

    public static void Main()
    {
        try
        {
            var configuration = BuildConfiguration();
            var serviceProvider = DependencyInjection(configuration);
            var query = SqlQuery.Select<Product>();
            Console.WriteLine(query);
            var files = serviceProvider.GetRequiredService<FileHandler>();
            foreach (var file in files.GetFiles())
            {
                Console.WriteLine(file);
                if (file.Contains("products_create")) Console.WriteLine("\tProducts for create");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}