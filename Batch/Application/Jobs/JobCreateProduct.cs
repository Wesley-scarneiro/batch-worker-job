using Batch.Repository.Interface;
using Batch.Application.Interface;
using Batch.Domain.Enums;
using Batch.Domain.Models;
using Batch.Application.Notifications.Interfaces;
using Batch.Application.Notifications;
using Batch.Application.Interfaces;

namespace Batch.Application.Jobs;

public class JobCreateProduct : IJob
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
        var files = await _fileHandler.GetFiles(TypeProduct.PRODUCT, Operation.CREATE);
        if (files.Any())
        {
            foreach (var file in files)
            {
                var records = await _fileHandler.ReadFile<Product>(file.Name);
                if (records.Any())
                {
                    await _database.Create(records);
                    await _fileHandler.MoveFile(file.Name);
                    continue;
                }
                _notifier.AddNotification(new Notification(NotificationLevel.WARNING, $"File '{file.Name}' is empty'"));
            }
            return true;
        }
        _notifier.AddNotification(new Notification(NotificationLevel.WARNING, $"Empty '{GetType().Name}' files"));
        return false;
    }
}
