using Batch.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Batch.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection service, IConfiguration configuration)
    {
        var fileHandler = new FileHandler(configuration["Appsettings:FilesPath:Input"], configuration["Appsettings:FilesPath:Ouput"]);
        service.AddSingleton(fileHandler);
        return service;
    }
}
