using Batch.Application;
using Batch.Application.Interfaces;
using Batch.Application.Notifications;
using Batch.Application.Notifications.Interfaces;
using Batch.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Batch.Extensions;
public static class ApplicationExtensions
{
    public static IServiceCollection AddAplication(this IServiceCollection service)
    {
        service.AddSingleton<IFileHandler, FileHandler>()
            .AddSingleton<IWorker, Worker>()
            .AddSingleton<INotifier, Notifier>();
        return service;
    }
}
