using System.Collections.ObjectModel;

namespace Batch.Application.Notifications.Interfaces;
public interface INotifier
{
    public ReadOnlyCollection<Notification> Notifications {get;}
    public bool HasNotification {get;}
    public void AddNotification(Notification notification);
    public bool HasNotificationType(NotificationLevel level);
}
