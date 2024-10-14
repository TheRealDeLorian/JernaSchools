using JernaClassLib.MetricsNLogs;
using Microsoft.Extensions.Logging;

namespace JernaClassLib;

public delegate void UserChangeEventHandler(User user);
public delegate void CartTotalChangedEventHandler(int newTotal);

public class EventService
{
    private readonly ILogger<EventService> _logger;
    public event UserChangeEventHandler? UserChangeEvent;
    public event CartTotalChangedEventHandler? CartTotalChanged;
    public EventService()
    {
        
    }
    public EventService(ILogger<EventService> logger)
    {
        _logger = logger;
    }

    public void RaiseUserChangeEvent(User user)
    {
        JernaLogs.LogUserChange(_logger, user.Username);
        UserChangeEvent?.Invoke(user);
    }

    public void RaiseCartTotalChangeEvent(int newTotal)
    {
        JernaLogs.LogAddedToCart(_logger, newTotal);
        CartTotalChanged?.Invoke(newTotal);
    }
}
