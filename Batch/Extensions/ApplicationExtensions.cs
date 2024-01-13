using Batch.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Batch.Extensions;
public static class ApplicationExtensions
{
    public static IServiceCollection AddAplication(this IServiceCollection service)
    {
        service.AddSingleton<FileHandler>();
        return service;
    }
}
