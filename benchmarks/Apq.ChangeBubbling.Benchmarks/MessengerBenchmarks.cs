using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Messaging;

namespace Apq.ChangeBubbling.Benchmarks;

/// <summary>
/// 消息系统性能测试
/// </summary>
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net60)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net90)]
public class MessengerBenchmarks
{
    private BubblingChange _change;

    [GlobalSetup]
    public void Setup()
    {
        ChangeMessenger.Reset();
        _change = new BubblingChange
        {
            PropertyName = "TestProperty",
            Kind = NodeChangeKind.PropertyUpdate,
            NewValue = 42
        };
    }

    [Benchmark]
    public void Publish_SingleChange()
    {
        ChangeMessenger.Publish(_change);
    }

    [Benchmark]
    public BubblingChangeMessage RentAndReturn_Message()
    {
        var msg = BubblingChangeMessage.Rent(_change);
        msg.Return();
        return msg;
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        ChangeMessenger.Reset();
    }
}