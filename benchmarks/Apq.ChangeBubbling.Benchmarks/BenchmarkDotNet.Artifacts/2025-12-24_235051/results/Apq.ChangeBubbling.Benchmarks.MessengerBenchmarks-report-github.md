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
| Method                | Job      | Runtime  | Mean      | Error      | StdDev    | Gen0   | Allocated |
|---------------------- |--------- |--------- |----------:|-----------:|----------:|-------:|----------:|
| Publish_SingleChange  | .NET 6.0 | .NET 6.0 | 949.04 ns |  11.809 ns |  7.028 ns | 0.0095 |     456 B |
| RentAndReturn_Message | .NET 6.0 | .NET 6.0 |  32.09 ns |   1.670 ns |  1.105 ns |      - |         - |
| Publish_SingleChange  | .NET 8.0 | .NET 8.0 | 652.31 ns |  41.244 ns | 27.280 ns | 0.0095 |     456 B |
| RentAndReturn_Message | .NET 8.0 | .NET 8.0 |  35.76 ns |   2.008 ns |  1.328 ns |      - |         - |
| Publish_SingleChange  | .NET 9.0 | .NET 9.0 | 540.11 ns | 118.009 ns | 78.056 ns | 0.0095 |     456 B |
| RentAndReturn_Message | .NET 9.0 | .NET 9.0 |  19.35 ns |   0.944 ns |  0.624 ns |      - |         - |
