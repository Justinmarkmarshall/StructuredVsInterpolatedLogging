# StructuredVsInterpolatedLoggingBench

| Method                                          | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------------------------------ |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| Enabled_LoggerMessage_Define                    | 142.319 ns | 4.8325 ns | 4.5204 ns |  1.00 |    0.00 | 0.0191 |     120 B |        1.00 |
| Enabled_Structured_Template                     | 218.301 ns | 7.7286 ns | 7.2294 ns |  1.54 |    0.07 | 0.0353 |     224 B |        1.87 |
| Enabled_Interpolated_Handler                    | 100.549 ns | 3.0636 ns | 2.8657 ns |  0.71 |    0.03 | 0.0191 |     120 B |        1.00 |
| Enabled_Preformatted_String                     | 146.771 ns | 7.8847 ns | 7.3753 ns |  1.03 |    0.06 | 0.0279 |     176 B |        1.47 |
| Disabled_LoggerMessage_Define                   |   4.821 ns | 0.4442 ns | 0.4155 ns |  0.03 |    0.00 |      - |         - |        0.00 |
| Disabled_Structured_Template                    |  48.144 ns | 2.8918 ns | 2.7050 ns |  0.34 |    0.02 | 0.0166 |     104 B |        0.87 |
| Disabled_Interpolated_Handler                   |  98.058 ns | 6.0689 ns | 5.6768 ns |  0.69 |    0.04 | 0.0191 |     120 B |        1.00 |
| Disabled_Preformatted_String                    | 143.013 ns | 7.9650 ns | 7.0608 ns |  1.01 |    0.06 | 0.0279 |     176 B |        1.47 |
| Enabled_Interpolated_Vs_Structured_Interpolated |  67.101 ns | 3.4137 ns | 3.0262 ns |  0.47 |    0.03 | 0.0191 |     120 B |        1.00 |
| Enabled_Interpolated_Vs_Structured_Structured   | 155.796 ns | 8.6919 ns | 8.1304 ns |  1.10 |    0.07 | 0.0331 |     208 B |        1.73 |
