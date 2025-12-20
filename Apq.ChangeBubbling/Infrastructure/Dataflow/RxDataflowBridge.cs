using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Messaging;

namespace Apq.ChangeBubbling.Infrastructure.Dataflow;

/// <summary>
/// 将 Rx 的缓冲事件流桥接到 Dataflow 管线，既利用 Rx 的时间/数量窗口，又通过 Dataflow 的有界容量与并行度降低 GC 压力。
/// </summary>
public static class RxDataflowBridge
{
    /// <summary>
    /// 订阅指定环境的缓冲事件流，并通过 Dataflow ActionBlock 按批处理。
    /// 返回 IDisposable，用于一次性清理订阅与管线。
    /// </summary>
    public static IDisposable StartBufferedProcessing(
        string envName,
        TimeSpan timeWindow,
        int countWindow,
        Action<IList<BubblingChange>> batchHandler,
        int boundedCapacity = 1024,
        int maxDegreeOfParallelism = 1)
    {
        var options = new ExecutionDataflowBlockOptions
        {
            BoundedCapacity = Math.Max(1, boundedCapacity),
            MaxDegreeOfParallelism = Math.Max(1, maxDegreeOfParallelism)
        };

        var block = new ActionBlock<IList<BubblingChange>>(batchHandler, options);

        var sub = ChangeMessenger
            .AsBufferedObservable(timeWindow, countWindow, envName)
            .Subscribe(batch =>
            {
                if (batch.Count == 0) return;
                // 尝试非阻塞提交；满负荷时可根据策略丢弃/阻塞/合并
                block.Post(batch);
            });

        return new CompositeDisposable(() =>
        {
            sub.Dispose();
            block.Complete();
        });
    }

    private sealed class CompositeDisposable : IDisposable
    {
        private readonly Action _dispose;
        public CompositeDisposable(Action dispose) => _dispose = dispose;
        public void Dispose() => _dispose();
    }
}


