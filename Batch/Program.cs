using System;
using Microsoft.Extensions.Configuration;

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

    private static void Main()
    {
        try
        {
            var configuration = BuildConfiguration();
            Console.WriteLine($"{configuration["Appsettings:Key"]}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}