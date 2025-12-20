using System;
using System.Threading.Tasks.Dataflow;
using Apq.ChangeBubbling.Abstractions;

namespace Apq.ChangeBubbling.Infrastructure.Dataflow;

/// <summary>
/// 基于 Dataflow 的冒泡事件背压管线：缓冲 + 处理，支持可配置并行度与容量。
/// </summary>
public sealed class ChangeDataflowPipeline : IDisposable
{
    private readonly BufferBlock<BubblingChange> _buffer;
    private readonly ActionBlock<BubblingChange> _processor;

    public ChangeDataflowPipeline(Action<BubblingChange> handler, int boundedCapacity = 10_000, int maxDegreeOfParallelism = 1)
    {
        // Normalize invalid inputs
        var capacity = boundedCapacity <= 0 ? DataflowBlockOptions.Unbounded : boundedCapacity;
        var degree = Math.Max(1, maxDegreeOfParallelism);

        var options = new ExecutionDataflowBlockOptions
        {
            BoundedCapacity = capacity,
            MaxDegreeOfParallelism = degree
        };

        _buffer = new BufferBlock<BubblingChange>(new DataflowBlockOptions { BoundedCapacity = capacity });
        _processor = new ActionBlock<BubblingChange>(handler, options);
        _buffer.LinkTo(_processor, new DataflowLinkOptions { PropagateCompletion = true });
    }

    public bool Post(BubblingChange change) => _buffer.Post(change);

    public void Complete() => _buffer.Complete();

    public void Dispose()
    {
        _buffer.Complete();
        _processor.Complete();
    }
}


