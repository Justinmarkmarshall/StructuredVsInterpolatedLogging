using System;
using Microsoft.Extensions.Logging;

public sealed class NullProvider(LogLevel minLevel) : ILoggerProvider
{
    private readonly LogLevel _minLevel = minLevel;

    public ILogger CreateLogger(string categoryName) => new NullLogger(_minLevel);

    public void Dispose() { }

    private sealed class NullLogger(LogLevel minLevel) : ILogger
    {
        private readonly LogLevel _minLevel = minLevel;

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLevel;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                                Exception? exception, Func<TState, Exception?, string> formatter)
        {
            // Simulate a real provider: when enabled, perform formatting work,
            // but do not write to I/O. This isolates API overhead vs string creation.
            if (!IsEnabled(logLevel)) return;

            // Call the formatter to incur formatting cost comparable to real sinks.
            _ = formatter(state, exception);
        }

        private sealed class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();
            public void Dispose() { }
        }
    }
}
