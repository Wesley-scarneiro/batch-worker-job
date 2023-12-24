using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    /*
        Initializes application settings
    */
    private static IConfiguration BuildConfiguration()
    {
        var environment = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")) ? "Development" : Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"))
            .AddJsonFile($"appsettings-{environment}.json", optional: true, reloadOnChange: true)
            .Build();
        return configuration;
    }

    /*
        Dependency injection settings
    */
    private static IServiceProvider DependencyInjection(IConfiguration configuration)
    {
        var serviceProvider = new ServiceCollection()
            .BuildServiceProvider();
        return serviceProvider;
    }

    private static void Main()
    {
        try
        {
            var configuration = BuildConfiguration();
            var serviceProvider = DependencyInjection(configuration);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}