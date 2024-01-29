namespace Batch.Application.Notifications;
public class Notification
{
    public NotificationLevel Level {get; init;}
    public string Message {get; init;}
    private static int _counter = 0;
    public int Id {get; init;}

    public Notification(NotificationLevel level, string message)
    {
        Level = level;
        Message = message;
        Id = ++_counter;
    }

    public override string ToString()
    {
        var str = @$"Notification {Id}
Level: {Level}
Message: {Message}";
        return str;
    }
}
