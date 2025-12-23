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
| Method                | Runtime  | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |--------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| ListNode_Add          | .NET 9.0 | 2,124.4 ns |  82.14 ns | 236.98 ns | 2,137.1 ns |  0.60 |    0.15 | 0.0229 | 0.0114 |     832 B |        1.00 |
| ListNode_Add          | .NET 6.0 | 3,714.0 ns | 300.05 ns | 870.51 ns | 3,503.7 ns |  1.05 |    0.35 | 0.0229 | 0.0076 |     832 B |        1.00 |
| ListNode_Add          | .NET 8.0 | 2,485.8 ns | 115.10 ns | 332.08 ns | 2,458.1 ns |  0.71 |    0.19 | 0.0153 | 0.0114 |     832 B |        1.00 |
| ListNode_Add          | .NET 9.0 | 2,180.5 ns |  89.86 ns | 259.26 ns | 2,118.4 ns |  0.62 |    0.16 | 0.0229 | 0.0114 |     832 B |        1.00 |
|                       |          |            |           |           |            |       |         |        |        |           |             |
| ListNode_AddAndRemove | .NET 9.0 |   745.1 ns |  14.87 ns |  35.92 ns |   740.2 ns |  0.53 |    0.04 | 0.0229 |      - |    1208 B |        1.00 |
| ListNode_AddAndRemove | .NET 6.0 | 1,412.8 ns |  28.27 ns |  68.28 ns | 1,393.7 ns |  1.00 |    0.07 | 0.0229 |      - |    1208 B |        1.00 |
| ListNode_AddAndRemove | .NET 8.0 | 1,084.8 ns |  22.11 ns |  64.14 ns | 1,081.6 ns |  0.77 |    0.06 | 0.0229 |      - |    1208 B |        1.00 |
| ListNode_AddAndRemove | .NET 9.0 |   761.1 ns |  16.87 ns |  48.93 ns |   760.5 ns |  0.54 |    0.04 | 0.0229 |      - |    1208 B |        1.00 |
|                       |          |            |           |           |            |       |         |        |        |           |             |
| DictNode_Put          | .NET 9.0 | 3,371.8 ns | 182.87 ns | 524.69 ns | 3,330.1 ns |  0.73 |    0.18 | 0.0343 | 0.0305 |     888 B |        1.00 |
| DictNode_Put          | .NET 6.0 | 4,785.7 ns | 332.70 ns | 954.57 ns | 4,428.7 ns |  1.04 |    0.29 | 0.0153 | 0.0114 |     888 B |        1.00 |
| DictNode_Put          | .NET 8.0 | 3,397.1 ns | 142.30 ns | 405.98 ns | 3,333.0 ns |  0.74 |    0.17 | 0.0343 | 0.0305 |     888 B |        1.00 |
| DictNode_Put          | .NET 9.0 | 3,480.5 ns | 145.11 ns | 413.99 ns | 3,431.6 ns |  0.76 |    0.17 | 0.0153 | 0.0114 |     888 B |        1.00 |
|                       |          |            |           |           |            |       |         |        |        |           |             |
| DictNode_PutAndRemove | .NET 9.0 | 3,836.1 ns | 162.08 ns | 465.04 ns | 3,843.1 ns |  0.68 |    0.11 | 0.0458 | 0.0229 |    1648 B |        1.00 |
| DictNode_PutAndRemove | .NET 6.0 | 5,707.8 ns | 215.52 ns | 625.27 ns | 5,660.7 ns |  1.01 |    0.16 | 0.0305 | 0.0153 |    1648 B |        1.00 |
| DictNode_PutAndRemove | .NET 8.0 | 4,037.0 ns | 140.00 ns | 403.93 ns | 3,958.9 ns |  0.72 |    0.11 | 0.0305 | 0.0267 |    1648 B |        1.00 |
| DictNode_PutAndRemove | .NET 9.0 | 3,306.4 ns |  86.11 ns | 248.44 ns | 3,267.4 ns |  0.59 |    0.08 | 0.0305 | 0.0229 |    1648 B |        1.00 |
