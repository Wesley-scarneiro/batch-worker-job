using System.Collections.ObjectModel;

namespace Batch.Application.Notifications.Interfaces;
public interface INotifiable
{
    public ReadOnlyCollection<Notification> Notifications {get;}
    public bool HasNotification {get;}
}
