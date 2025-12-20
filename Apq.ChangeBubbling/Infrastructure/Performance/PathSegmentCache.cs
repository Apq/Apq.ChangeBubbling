using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Apq.ChangeBubbling.Infrastructure.Performance;

/// <summary>
/// PathSegments 单元素数组缓存，避免高频场景下重复分配。
/// </summary>
internal static class PathSegmentCache
{
    // 缓存单元素数组，key 为属性名
    // 预分配容量减少扩容开销，使用 Ordinal 比较器提升字符串查找性能
    private static readonly ConcurrentDictionary<string, string[]> _singleSegmentCache
        = new(Environment.ProcessorCount, 256, StringComparer.Ordinal);

    // 缓存索引格式字符串 "[0]" ~ "[999]"
    private static readonly string[] _indexStrings = new string[1000];

    // 预缓存索引单元素数组（0-999）
    private static readonly string[][] _indexSegments = new string[1000][];

    // 超出预缓存范围的索引使用字典
    // 预分配容量减少扩容开销
    private static readonly ConcurrentDictionary<int, string[]> _indexSegmentCache
        = new(Environment.ProcessorCount, 128);

    // 缓存容量限制，防止动态属性名导致内存泄漏
    private const int MaxSingleSegmentCacheSize = 10000;
    private const int MaxIndexSegmentCacheSize = 10000;

    // 使用原子计数器跟踪缓存大小，避免 ConcurrentDictionary.Count 的 O(n) 开销
    private static int _singleSegmentCacheCount;
    private static int _indexSegmentCacheCount;

    // 预热的常用属性名
    private static readonly string[] CommonPropertyNames = new[]
    {
        "Items", "Data", "Value", "Values", "Keys", "Count",
        "Name", "Id", "Type", "State", "Status", "Result",
        "Children", "Parent", "Root", "Index", "Key"
    };

    static PathSegmentCache()
    {
        // 预生成常用索引字符串和单元素数组
        for (var i = 0; i < _indexStrings.Length; i++)
        {
            _indexStrings[i] = $"[{i}]";
            _indexSegments[i] = new[] { _indexStrings[i] };
        }

        // 预热常用属性名
        foreach (var name in CommonPropertyNames)
        {
            var arr = new[] { name };
            if (_singleSegmentCache.TryAdd(name, arr))
            {
                Interlocked.Increment(ref _singleSegmentCacheCount);
            }
        }
    }

    /// <summary>
    /// 获取单元素 PathSegments 数组（缓存）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[] GetSingle(string segment)
    {
        if (_singleSegmentCache.TryGetValue(segment, out var cached))
            return cached;

        // 使用原子计数器检查容量（允许轻微超出，避免竞态）
        if (Volatile.Read(ref _singleSegmentCacheCount) >= MaxSingleSegmentCacheSize)
            return new[] { segment };

        var arr = new[] { segment };
        if (_singleSegmentCache.TryAdd(segment, arr))
        {
            Interlocked.Increment(ref _singleSegmentCacheCount);
        }
        return arr;
    }

    /// <summary>
    /// 获取索引格式字符串 "[index]"（缓存常用索引）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetIndexString(int index)
    {
        if (index >= 0 && index < _indexStrings.Length)
            return _indexStrings[index];

        return $"[{index}]";
    }

    /// <summary>
    /// 获取索引单元素 PathSegments 数组（缓存）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[] GetIndexSegment(int index)
    {
        // 快速路径：预缓存范围内直接返回
        if (index >= 0 && index < _indexSegments.Length)
            return _indexSegments[index];

        // 慢速路径：超出范围使用字典
        if (_indexSegmentCache.TryGetValue(index, out var cached))
            return cached;

        // 使用原子计数器检查容量
        if (Volatile.Read(ref _indexSegmentCacheCount) >= MaxIndexSegmentCacheSize)
        {
            var indexStr = $"[{index}]";
            return new[] { indexStr };
        }

        var str = $"[{index}]";
        var arr = new[] { str };
        if (_indexSegmentCache.TryAdd(index, arr))
        {
            Interlocked.Increment(ref _indexSegmentCacheCount);
        }
        return arr;
    }

    /// <summary>
    /// 清除缓存（用于测试）。
    /// </summary>
    public static void Clear()
    {
        _singleSegmentCache.Clear();
        _indexSegmentCache.Clear();
        Volatile.Write(ref _singleSegmentCacheCount, 0);
        Volatile.Write(ref _indexSegmentCacheCount, 0);
    }
}
