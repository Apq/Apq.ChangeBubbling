```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26200.7462)
Unknown processor
.NET SDK 9.0.308
  [Host]   : .NET 9.0.11 (9.0.1125.51716), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 6.0 : .NET 6.0.36 (6.0.3624.51421), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.22 (8.0.2225.52707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.11 (9.0.1125.51716), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

IterationCount=10  LaunchCount=1  WarmupCount=5  

```
| Method                       | Job      | Runtime  | Mean      | Error     | StdDev    | Gen0   | Allocated |
|----------------------------- |--------- |--------- |----------:|----------:|----------:|-------:|----------:|
| CreateBubblingChange         | .NET 6.0 | .NET 6.0 |  5.560 ns | 0.0448 ns | 0.0235 ns | 0.0005 |      24 B |
| CreateBubblingChangeWithPath | .NET 6.0 | .NET 6.0 | 10.042 ns | 0.3811 ns | 0.2520 ns | 0.0014 |      72 B |
| CreateBubblingChange         | .NET 8.0 | .NET 8.0 |  3.217 ns | 0.0704 ns | 0.0368 ns | 0.0005 |      24 B |
| CreateBubblingChangeWithPath | .NET 8.0 | .NET 8.0 |  5.358 ns | 0.4777 ns | 0.3159 ns | 0.0014 |      72 B |
| CreateBubblingChange         | .NET 9.0 | .NET 9.0 |  3.269 ns | 0.1355 ns | 0.0896 ns | 0.0005 |      24 B |
| CreateBubblingChangeWithPath | .NET 9.0 | .NET 9.0 |  5.092 ns | 0.0991 ns | 0.0518 ns | 0.0014 |      72 B |
