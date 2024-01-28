namespace Batch.Application.Notifications;
public class Notification
{
    public NotificationLevel Level {get; init;}
    public string Message {get; init;}

    public Notification(NotificationLevel level, string message)
    {
        Level = level;
        Message = message;
    }
}
