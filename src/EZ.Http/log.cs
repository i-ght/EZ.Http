/*namespace EZ.Http;

internal enum LogLevel
{
    None,
    Trace,
    Debug,
    Information,
    Warning,
    Error,
    Critical
}

internal abstract class Logger
{
    public string Name { get; }
    public LogLevel LogLevelThreshold { get; }

    public abstract void WriteEntry(
        LogLevel logLevel,
        string fmt,
        params object?[] args
    );
    public abstract void WriteError(Exception e);

    public Logger(string name, LogLevel logLevel) =>
        (Name, LogLevelThreshold) = (name, logLevel);
}

internal readonly record struct ConsoleColors(
    ConsoleColor? Foreground,
    ConsoleColor? Background
);

internal static class ConsLog
{
    private static ConsoleColors ColorsForLogLevel(
        LogLevel logLevel) =>
            logLevel switch {
                LogLevel.Critical => new ConsoleColors(ConsoleColor.White, ConsoleColor.Red),
                LogLevel.Error => new ConsoleColors(ConsoleColor.Black, ConsoleColor.Red),
                LogLevel.Warning => new ConsoleColors(ConsoleColor.Yellow, ConsoleColor.Black),
                LogLevel.Information => new ConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Black),
                LogLevel.Debug => new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black),
                LogLevel.Trace => new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black),
                _ => new ConsoleColors(default, default)
            };
    private static string LogLevelStr(
        LogLevel logLevel
    ) => logLevel switch {
        LogLevel.Trace => "trce",
        LogLevel.Debug => "dbug",
        LogLevel.Information => "info",
        LogLevel.Warning => "warn",
        LogLevel.Error => "fail",
        LogLevel.Critical => "crit",
        _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
    };

    private static readonly object Lock = new object();

    public static void WriteEntry(
        LogLevel logLevel,
        string loggerName,
        LogLevel logToStandardErrorThreshold,
        string fmt,
        params object?[] args)
    {
        var colors = ColorsForLogLevel(logLevel);
        var levelStr = LogLevelStr(logLevel);

        var consoleWriter =
            logLevel >= logToStandardErrorThreshold
            ? Console.Error
            : Console.Out;

        lock (Lock) {
            var foreground = Console.ForegroundColor;
            var background = Console.BackgroundColor;

            Console.ForegroundColor =
                colors.Foreground.HasValue
                ? colors.Foreground.Value
                : foreground;
            Console.BackgroundColor =
                colors.Background.HasValue
                ? colors.Background.Value
                : background;

            consoleWriter.Write(levelStr);

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;

            consoleWriter.WriteLine(
                "{0}{1}",
                ": ",
                loggerName
            );
            consoleWriter.Write("      ");
            consoleWriter.WriteLine(
                fmt,
                args
            );
        }
    }
}

internal class ConsoleLogger : Logger
{
    private readonly LogLevel _logToStandardErrorThreshold;

    private void _WriteEntry(
        LogLevel logLevel,
        string fmt,
        params object?[] args)
    {
        if (logLevel < LogLevelThreshold) {
            return;
        }

        ConsLog.WriteEntry(
            logLevel,
            Name,
            _logToStandardErrorThreshold,
            fmt,
            args
        );
    }

    public override void WriteEntry(
        LogLevel logLevel,
        string fmt,
        params object?[] args)
    {
        _WriteEntry(
            logLevel,
            fmt,
            args
        );
    }

    public override void WriteError(
        Exception ex)
    {
        if (LogLevelThreshold < LogLevel.Error) {
            return;
        }

        _WriteEntry(
            LogLevel.Error,
            ex.ToString()
        );
    }


    public ConsoleLogger(
        string name,
        LogLevel logLevel,
        LogLevel logToStdErrThreshold) : base(name, logLevel)
    {
        _logToStandardErrorThreshold = logToStdErrThreshold;
    }
}
*/