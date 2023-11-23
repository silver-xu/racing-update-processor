namespace RacingUpdateProcessor.Notifications.Interfaces;

/// <summary>
/// Interface to notify 3rd party that a task has been completed
///
/// The abstraction is around tech / protocol independence, the notification
/// could be done in Queues, Emails, SMS and processed either automatically
/// or manually
/// </summary>
public interface INotificationProvider
{
    /// <summary>
    /// Notify completion of a task
    /// </summary>
    /// <param name="content">the content of the notification</param>
    /// <returns>void</returns>
    public Task Notify(string content);
}
