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
| Method                | Job      | Runtime  | Mean       | Error       | StdDev      | Gen0   | Gen1   | Allocated |
|---------------------- |--------- |--------- |-----------:|------------:|------------:|-------:|-------:|----------:|
| ListNode_Add          | .NET 6.0 | .NET 6.0 | 2,201.6 ns |   402.04 ns |   210.28 ns | 0.0191 | 0.0076 |     808 B |
| ListNode_AddAndRemove | .NET 6.0 | .NET 6.0 | 2,112.8 ns |   180.88 ns |   119.64 ns | 0.0229 |      - |    1208 B |
| DictNode_Put          | .NET 6.0 | .NET 6.0 | 3,771.1 ns | 2,225.80 ns | 1,164.14 ns | 0.0229 | 0.0114 |     888 B |
| DictNode_PutAndRemove | .NET 6.0 | .NET 6.0 | 5,702.9 ns | 1,921.20 ns | 1,004.83 ns | 0.0458 | 0.0191 |    1648 B |
| ListNode_Add          | .NET 8.0 | .NET 8.0 | 2,239.9 ns |   344.63 ns |   180.25 ns | 0.0267 | 0.0153 |     808 B |
| ListNode_AddAndRemove | .NET 8.0 | .NET 8.0 |   938.1 ns |    68.36 ns |    40.68 ns | 0.0229 |      - |    1208 B |
| DictNode_Put          | .NET 8.0 | .NET 8.0 | 2,835.5 ns |   612.63 ns |   364.57 ns | 0.0343 | 0.0305 |     888 B |
| DictNode_PutAndRemove | .NET 8.0 | .NET 8.0 | 3,420.0 ns |   661.31 ns |   393.54 ns | 0.0458 | 0.0229 |    1648 B |
| ListNode_Add          | .NET 9.0 | .NET 9.0 | 2,176.3 ns |   581.25 ns |   345.89 ns | 0.0191 | 0.0076 |     808 B |
| ListNode_AddAndRemove | .NET 9.0 | .NET 9.0 |   670.5 ns |    22.13 ns |    14.64 ns | 0.0238 |      - |    1208 B |
| DictNode_Put          | .NET 9.0 | .NET 9.0 | 2,916.3 ns |   565.34 ns |   336.42 ns | 0.0343 | 0.0305 |     888 B |
| DictNode_PutAndRemove | .NET 9.0 | .NET 9.0 | 3,488.2 ns |   573.60 ns |   341.34 ns | 0.0458 | 0.0229 |    1648 B |
