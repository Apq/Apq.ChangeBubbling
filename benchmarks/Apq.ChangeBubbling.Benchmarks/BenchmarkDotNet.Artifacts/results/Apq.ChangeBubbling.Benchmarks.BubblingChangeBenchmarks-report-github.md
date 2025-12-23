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
| Method                       | Runtime  | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------------- |--------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| CreateBubblingChange         | .NET 9.0 |  3.115 ns | 0.0487 ns | 0.0432 ns |  3.112 ns |  0.50 |    0.09 | 0.0005 |      24 B |        1.00 |
| CreateBubblingChange         | .NET 6.0 |  6.451 ns | 0.4350 ns | 1.2827 ns |  5.552 ns |  1.04 |    0.28 | 0.0005 |      24 B |        1.00 |
| CreateBubblingChange         | .NET 8.0 |  3.774 ns | 0.0983 ns | 0.1441 ns |  3.734 ns |  0.61 |    0.11 | 0.0005 |      24 B |        1.00 |
| CreateBubblingChange         | .NET 9.0 |  3.856 ns | 0.1038 ns | 0.2994 ns |  3.772 ns |  0.62 |    0.12 | 0.0005 |      24 B |        1.00 |
|                              |          |           |           |           |           |       |         |        |           |             |
| CreateBubblingChangeWithPath | .NET 9.0 |  5.098 ns | 0.0955 ns | 0.0797 ns |  5.099 ns |  0.38 |    0.04 | 0.0014 |      72 B |        1.00 |
| CreateBubblingChangeWithPath | .NET 6.0 | 13.469 ns | 0.4350 ns | 1.2825 ns | 13.154 ns |  1.01 |    0.14 | 0.0014 |      72 B |        1.00 |
| CreateBubblingChangeWithPath | .NET 8.0 |  5.861 ns | 0.1412 ns | 0.3936 ns |  5.783 ns |  0.44 |    0.05 | 0.0014 |      72 B |        1.00 |
| CreateBubblingChangeWithPath | .NET 9.0 |  5.855 ns | 0.1467 ns | 0.4303 ns |  5.706 ns |  0.44 |    0.05 | 0.0014 |      72 B |        1.00 |
