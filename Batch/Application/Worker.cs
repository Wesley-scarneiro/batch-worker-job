using Batch.Repository.Interface;
using Batch.Application.Interface;
using Batch.Application.Notifications.Interfaces;
using System.Collections.ObjectModel;
using Batch.Application.Notifications;
using Batch.Application.Interfaces;

namespace Batch.Application;
public class Worker : IWorker
{
    private Queue<IJob> _workQueue;
    private readonly IFileHandler _fileService;
    private readonly IDbContext _dbContext;
    private readonly INotifier _notifier;
    public ReadOnlyCollection<Notification> Notifications => _notifier.Notifications;
    public bool HasNotification => _notifier.HasNotification;

    public Worker(IFileHandler fileHandler, IDbContext dbContext, INotifier notifier)
    {
        _workQueue = new Queue<IJob>();
        _fileService = fileHandler;
        _dbContext = dbContext;
        _notifier = notifier;
    }

    public Worker CreateJob<T>() where T : IJob, new()
    {
        var job = new T();
        job.Init(_fileService, _dbContext, _notifier);
        _workQueue.Enqueue(job);
        return this;
    }

    public async Task<bool> Run()
    {
        var workStatus = true;
        while (_workQueue.Count > 0 && workStatus)
        {
            var job = _workQueue.Dequeue();
            await job.Run();
            workStatus = !_notifier.HasNotificationType(NotificationLevel.ERROR);
        }
        return workStatus;
    }
}