using Batch.Application.Interfaces;
using Batch.Application.Notifications.Interfaces;
using Batch.Repository.Interface;

namespace Batch.Application.Interface;
public interface IJob
{
    public void Init(IFileHandler fileService, IDbContext database, INotifier notifier);
    public Task<bool> Run();
}
