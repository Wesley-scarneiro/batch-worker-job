using Batch.Application.Interface;
using Batch.Application.Notifications.Interfaces;

namespace Batch.Application.Interfaces;
public interface IWorker : INotifiable
{
    public Task<bool> Run();
    public Worker CreateJob<T>() where T : IJob, new();
}
