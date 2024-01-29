using Batch.Repository.Interface;
using Batch.Application.Interface;
using Batch.Domain.Enums;
using Batch.Domain.Models;
using Batch.Application.Notifications.Interfaces;
using Batch.Application.Notifications;
using Batch.Application.Interfaces;

namespace Batch.Application.Jobs;

public class JobViewProduct : IJob
{
    private IFileHandler _fileHandler;
    private IDbContext _database;
    private INotifier _notifier;

    public void Init(IFileHandler fileHandler, IDbContext database, INotifier notifier)
    {
        _fileHandler = fileHandler;
        _database = database;
        _notifier = notifier;
    }

    public async Task<bool> Run()
    {
        var records = await _database.Read<Product>();
        var response = await _fileHandler.CreateFile(records);
        if (response) return true;
        _notifier.AddNotification(new Notification(NotificationLevel.ERROR, $"Unable to create a file through of job {GetType().Name}"));
        return false;
    }
}
