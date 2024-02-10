using Batch.Application.Interfaces;
using Batch.Application.Notifications.Interfaces;
using Batch.Repositories.Interface;

namespace Batch.Application.Interface;
public interface IJob
{
    public void Init(IFileHandler fileService, IRepository database, INotifier notifier);
    public Task<bool> Run();
}
