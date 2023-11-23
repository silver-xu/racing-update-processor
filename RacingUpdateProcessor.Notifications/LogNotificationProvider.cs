using Microsoft.Extensions.Logging;
using RacingUpdateProcessor.Notifications.Interfaces;

namespace RacingUpdateProcessor.Notifications;

public class LogNotificationProvider : INotificationProvider
{
    private readonly ILogger<LogNotificationProvider> _logger;

    public LogNotificationProvider(ILogger<LogNotificationProvider> logger)
    {
        _logger = logger;
    }

    public Task Notify(string content)
    {
        _logger.LogInformation(content);

        return Task.CompletedTask;
    }
}
