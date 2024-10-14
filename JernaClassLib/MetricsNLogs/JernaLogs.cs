using Microsoft.Extensions.Logging;

namespace JernaClassLib.MetricsNLogs;


public static partial class JernaLogs
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Starting {appName}.")]
    public static partial void LogApplicationAccess(ILogger logger, string appName);
    [LoggerMessage(Level = LogLevel.Information, Message = "New User Approaches: {userName}.")]
    public static partial void LogUserChange(ILogger logger, string userName);
    [LoggerMessage(Level = LogLevel.Information, Message = "Someone added to their cart, its now at this many items: {cartTotal}.")]
    public static partial void LogAddedToCart(ILogger logger, int cartTotal);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Threw exception {exceptionName}.")]
    public static partial void LogException(ILogger logger, string exceptionName);
}


