```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26200.7462)
Unknown processor
.NET SDK 9.0.308
  [Host]     : .NET 9.0.11 (9.0.1125.51716), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.11 (9.0.1125.51716), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-WNDOFP : .NET 6.0.36 (6.0.3624.51421), X64 RyuJIT AVX2
  Job-NLGNSZ : .NET 8.0.22 (8.0.2225.52707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-BBOSDX : .NET 9.0.11 (9.0.1125.51716), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                | Runtime  | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------------- |--------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| Publish_SingleChange  | .NET 9.0 | 628.56 ns | 13.200 ns | 37.873 ns | 630.05 ns |  0.66 |    0.04 | 0.0076 |     456 B |        1.00 |
| Publish_SingleChange  | .NET 6.0 | 947.73 ns | 11.168 ns | 10.447 ns | 949.86 ns |  1.00 |    0.02 | 0.0076 |     456 B |        1.00 |
| Publish_SingleChange  | .NET 8.0 | 464.56 ns | 11.446 ns | 33.025 ns | 452.76 ns |  0.49 |    0.04 | 0.0086 |     456 B |        1.00 |
| Publish_SingleChange  | .NET 9.0 | 350.37 ns |  6.719 ns |  6.285 ns | 349.30 ns |  0.37 |    0.01 | 0.0091 |     456 B |        1.00 |
|                       |          |           |           |           |           |       |         |        |           |             |
| RentAndReturn_Message | .NET 9.0 |  17.23 ns |  0.630 ns |  1.857 ns |  17.09 ns |  0.83 |    0.09 |      - |         - |          NA |
| RentAndReturn_Message | .NET 6.0 |  20.64 ns |  0.292 ns |  0.325 ns |  20.54 ns |  1.00 |    0.02 |      - |         - |          NA |
| RentAndReturn_Message | .NET 8.0 |  37.66 ns |  0.760 ns |  0.780 ns |  37.87 ns |  1.83 |    0.05 |      - |         - |          NA |
| RentAndReturn_Message | .NET 9.0 |  10.59 ns |  0.176 ns |  0.165 ns |  10.56 ns |  0.51 |    0.01 |      - |         - |          NA |
