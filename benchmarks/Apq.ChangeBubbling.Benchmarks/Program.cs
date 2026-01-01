using BenchmarkDotNet.Running;
using Apq.ChangeBubbling.Benchmarks;

// 运行所有基准测试
BenchmarkSwitcher.FromAssembly(typeof(BubblingChangeBenchmarks).Assembly).Run(args, new BenchmarkConfig());