using BenchmarkDotNet.Attributes;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Core;
using Apq.ChangeBubbling.Nodes;

namespace Apq.ChangeBubbling.Benchmarks;

/// <summary>
/// 节点操作性能测试
/// </summary>
[Config(typeof(BenchmarkConfig))]
public class NodeBenchmarks
{
    private ListBubblingNode<int> _listNode = null!;
    private DictionaryBubblingNode<string, int> _dictNode = null!;

    [GlobalSetup]
    public void Setup()
    {
        _listNode = new ListBubblingNode<int>("TestList");
        _dictNode = new DictionaryBubblingNode<string, int>("TestDict");
    }

    [Benchmark]
    public void ListNode_Add()
    {
        _listNode.Add(42);
    }

    [Benchmark]
    public void ListNode_AddAndRemove()
    {
        _listNode.Add(42);
        _listNode.Remove(42);
    }

    [Benchmark]
    public void DictNode_Put()
    {
        var key = Guid.NewGuid().ToString();
        _dictNode.Put(key, 42);
    }

    [Benchmark]
    public void DictNode_PutAndRemove()
    {
        var key = Guid.NewGuid().ToString();
        _dictNode.Put(key, 42);
        _dictNode.Remove(key);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        // 重新创建节点以清理数据
        _listNode = new ListBubblingNode<int>("TestList");
        _dictNode = new DictionaryBubblingNode<string, int>("TestDict");
    }
}