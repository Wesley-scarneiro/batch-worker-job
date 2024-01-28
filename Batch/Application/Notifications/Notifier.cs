using System.Collections.ObjectModel;
using Batch.Application.Notifications.Interfaces;

namespace Batch.Application.Notifications;
public class Notifier : INotifier
{
    private List<Notification> _notifications;
    public ReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
    public bool HasNotification => _notifications.Any();

    public Notifier()
    {
        _notifications = new ();
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public bool HasNotificationType(NotificationLevel level)
    {
        return _notifications.Any(n => n.Level == level);
    }
}
