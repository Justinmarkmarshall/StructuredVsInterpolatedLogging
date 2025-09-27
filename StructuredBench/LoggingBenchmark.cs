using System;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

[MemoryDiagnoser]
[WarmupCount(5)]
[IterationCount(15)]
public class LoggingBenchmarks
{
    private ILogger _enabledLogger = default!;
    private ILogger _disabledLogger = default!;

    private static readonly Action<ILogger, int, string, decimal, Exception?> LogOrderProcessed =
        LoggerMessage.Define<int, string, decimal>(
            LogLevel.Information,
            new EventId(1001, nameof(LogOrderProcessed)),
            "Processed order {OrderId} for {Customer} at {Total}");

    private int _orderId;
    private string _customer = null!;
    private decimal _total;

    [GlobalSetup]
    public void Setup()
    {
        _orderId = 12345;
        _customer = "Frodo Baggins";
        _total = 42.50m;

        // Enabled logger: Information and above
        var enabledFactory = LoggerFactory.Create(b =>
        {
            b.ClearProviders();
            b.AddProvider(new NullProvider(LogLevel.Information));
            b.SetMinimumLevel(LogLevel.Information);
        });

        // Disabled logger: filter out Information (set to Warning)
        var disabledFactory = LoggerFactory.Create(b =>
        {
            b.ClearProviders();
            b.AddProvider(new NullProvider(LogLevel.Warning));
            b.SetMinimumLevel(LogLevel.Warning);
        });

        _enabledLogger = enabledFactory.CreateLogger("Bench.Enabled");
        _disabledLogger = disabledFactory.CreateLogger("Bench.Disabled");
    }

    // -------- Enabled (work happens) --------
    [Benchmark(Baseline = true)]
    public void Enabled_LoggerMessage_Define()
        => LogOrderProcessed(_enabledLogger, _orderId, _customer, _total, null);

    [Benchmark]
    public void Enabled_Structured_Template()
        => _enabledLogger.LogInformation("Processed order {OrderId} for {Customer} at {Total}",
                                         _orderId, _customer, _total);

    [Benchmark]
    public void Enabled_Interpolated_Handler()
        => _enabledLogger.LogInformation($"Processed order {_orderId} for {_customer} at {_total}");

    [Benchmark]
    public void Enabled_Preformatted_String()
    {
        var msg = string.Format("Processed order {0} for {1} at {2}", _orderId, _customer, _total);
        _enabledLogger.LogInformation(msg);
    }

    // -------- Disabled (filtered out) --------
    [Benchmark]
    public void Disabled_LoggerMessage_Define()
        => LogOrderProcessed(_disabledLogger, _orderId, _customer, _total, null);

    [Benchmark]
    public void Disabled_Structured_Template()
        => _disabledLogger.LogInformation("Processed order {OrderId} for {Customer} at {Total}",
                                          _orderId, _customer, _total);

    [Benchmark]
    public void Disabled_Interpolated_Handler()
        => _disabledLogger.LogInformation($"Processed order {_orderId} for {_customer} at {_total}");

    [Benchmark]
    public void Disabled_Preformatted_String()
    {
        var msg = string.Format("Processed order {0} for {1} at {2}", _orderId, _customer, _total);
        _disabledLogger.LogInformation(msg);
    }

    [Benchmark]
    public void Enabled_Interpolated_Vs_Structured_Interpolated()
    {
        _enabledLogger.LogInformation($"This is an interpolated log with numbers {1} and {2}");
    }

    [Benchmark]
    public void Enabled_Interpolated_Vs_Structured_Structured()
    {
        _enabledLogger.LogInformation("This is an interpolated log with numbers {Number1} and {Number2}", 1, 2);
    }
}
