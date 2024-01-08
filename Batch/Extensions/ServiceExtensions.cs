using Batch.Services;
using Batch.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Batch.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection service, IConfiguration configuration)
    {
        var localServices = new LocalFiles(configuration["Appsettings:FilesPath:Input"], configuration["Appsettings:FilesPath:Output"]);
        service.AddSingleton(localServices);
        return service;
    }
}
