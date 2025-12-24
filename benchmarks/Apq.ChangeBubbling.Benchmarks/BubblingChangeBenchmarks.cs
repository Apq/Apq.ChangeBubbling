using BenchmarkDotNet.Attributes;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Core;
using Apq.ChangeBubbling.Nodes;

namespace Apq.ChangeBubbling.Benchmarks;

/// <summary>
/// BubblingChange 结构体性能测试
/// </summary>
[Config(typeof(BenchmarkConfig))]
public class BubblingChangeBenchmarks
{
    [Benchmark]
    public BubblingChange CreateBubblingChange()
    {
        return new BubblingChange
        {
            PropertyName = "TestProperty",
            Kind = NodeChangeKind.PropertyUpdate,
            NewValue = 42
        };
    }

    [Benchmark]
    public BubblingChange CreateBubblingChangeWithPath()
    {
        return new BubblingChange
        {
            PropertyName = "TestProperty",
            Kind = NodeChangeKind.PropertyUpdate,
            PathSegments = new[] { "Root", "Child", "Leaf" },
            NewValue = 42
        };
    }
}