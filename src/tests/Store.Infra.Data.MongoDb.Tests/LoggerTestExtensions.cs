using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infra.Data.Tests;
public static class LoggerTestExtensions
{
    public static void AnyLogOfType<T>(this ILogger<T> logger, LogLevel level) where T : class
    {
        logger.Log(level, Arg.Any<EventId>(), Arg.Any<object>(), Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }

    public static bool ValidateReceivedLogType<T>(this ILogger<T> logger, LogLevel expecteLogLevel) where T: class
    {
        var msg = logger?.ReceivedCalls()?
                                              .SelectMany(x => x.GetArguments())?
                                              .Select(x => x?.ToString());
        return msg.First() == expecteLogLevel.ToString();
    }
}