using Batch.Repository.Interface;
using Batch.Application.Interface;
using Batch.Application.Notifications.Interfaces;
using System.Collections.ObjectModel;
using Batch.Application.Notifications;
using Batch.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Batch.Application;
public class Worker : IWorker
{
    private Queue<IJob> _workQueue;
    private bool _workStatus;
    private IJob? _currentJob;
    private readonly IFileHandler _fileService;
    private readonly IDbContext _dbContext;
    private readonly INotifier _notifier;
    private readonly ILogger<Worker> _logger;
    public ReadOnlyCollection<Notification> Notifications => _notifier.Notifications;
    public bool HasNotification => _notifier.HasNotification;

    public Worker(IFileHandler fileHandler, IDbContext dbContext, INotifier notifier, ILogger<Worker> logger)
    {
        _workQueue = new Queue<IJob>();
        _fileService = fileHandler;
        _dbContext = dbContext;
        _notifier = notifier;
        _logger = logger;
        _workStatus = false;
        _currentJob = null;
    }

    public Worker CreateJob<T>() where T : IJob, new()
    {
        var job = new T();
        job.Init(_fileService, _dbContext, _notifier);
        _workQueue.Enqueue(job);
        _logger.LogInformation("Job '{JOB}' added to the job queue", job.GetType().Name);
        return this;
    }

    public async Task<bool> Run()
    {
        try
        {
            var _workStatus = true;
            _logger.LogInformation("Starting queue execution with '{COUNT}' jobs", _workQueue.Count);
            while (_workQueue.Any() && _workStatus)
            {
                var _currentJob = _workQueue.Dequeue();
                var response = await _currentJob.Run();
                _logger.LogInformation("Job '{JOB}' completed with status '{RESPONSE}'", _currentJob.GetType().Name, response);
                _workStatus = !_notifier.HasNotificationType(NotificationLevel.ERROR);
            }
            _logger.LogInformation("Work queue execution finished with state '{WORKERSTATUS}'", _workStatus);
            _currentJob = null;
            return _workStatus;
        }
        catch(Exception)
        {
            _logger.LogCritical("Critical failure during execution of job {JOB}", _currentJob?.GetType().Name);
            throw;
        }
    }
}