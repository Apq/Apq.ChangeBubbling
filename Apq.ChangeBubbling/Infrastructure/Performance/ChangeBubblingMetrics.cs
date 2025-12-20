using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Apq.ChangeBubbling.Infrastructure.Performance;

/// <summary>
/// ChangeBubbling性能指标收集器。
/// </summary>
public static class ChangeBubblingMetrics
{
    /// <summary>
    /// 指数移动平均的平滑因子 (0-1)，值越大新数据权重越高。
    /// </summary>
    private const double EmaAlpha = 0.2;

    // 缓存容量限制
    private const int MaxKeyCacheSize = 10000;
    private const int MaxKey3CacheSize = 10000;

    // 原子计数器跟踪缓存大小
    private static int _keyCacheCount;
    private static int _key3CacheCount;

    private static readonly ConcurrentDictionary<string, long> _eventCounts = new();
    private static readonly ConcurrentDictionary<string, TimeSpan> _processingTimes = new();
    private static readonly ConcurrentDictionary<string, int> _activeSubscriptions = new();
    private static readonly ConcurrentDictionary<string, long> _filteredEvents = new();

    // 缓存常用的 key，避免重复字符串拼接
    private static readonly ConcurrentDictionary<(string, string?), string> _keyCache = new();
    private static readonly ConcurrentDictionary<(string, string, string?), string> _key3Cache = new();

    /// <summary>
    /// 获取或创建缓存的 key。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetOrCreateKey(string part1, string? part2)
    {
        var cacheKey = (part1, part2);
        if (_keyCache.TryGetValue(cacheKey, out var cached))
            return cached;

        var key = $"{part1}:{part2 ?? "Default"}";

        // 检查容量限制
        if (Volatile.Read(ref _keyCacheCount) >= MaxKeyCacheSize)
            return key;

        if (_keyCache.TryAdd(cacheKey, key))
        {
            Interlocked.Increment(ref _keyCacheCount);
        }
        return key;
    }

    /// <summary>
    /// 获取或创建缓存的三段 key。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetOrCreateKey3(string part1, string part2, string? part3)
    {
        var cacheKey = (part1, part2, part3);
        if (_key3Cache.TryGetValue(cacheKey, out var cached))
            return cached;

        var key = $"{part1}:{part2}:{part3 ?? "Default"}";

        // 检查容量限制
        if (Volatile.Read(ref _key3CacheCount) >= MaxKey3CacheSize)
            return key;

        if (_key3Cache.TryAdd(cacheKey, key))
        {
            Interlocked.Increment(ref _key3CacheCount);
        }
        return key;
    }

    /// <summary>
    /// 计算指数移动平均。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TimeSpan CalculateEma(TimeSpan oldValue, TimeSpan newValue)
    {
        // EMA = alpha * newValue + (1 - alpha) * oldValue
        var emaMs = EmaAlpha * newValue.TotalMilliseconds + (1 - EmaAlpha) * oldValue.TotalMilliseconds;
        return TimeSpan.FromMilliseconds(emaMs);
    }

    /// <summary>
    /// 记录事件处理。
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="processingTime">处理时间</param>
    /// <param name="context">上下文</param>
    public static void RecordEventProcessed(string eventType, TimeSpan processingTime, string? context = null)
    {
        var key = GetOrCreateKey(eventType, context);

        _eventCounts.AddOrUpdate(key, 1, static (k, v) => v + 1);

        // 只有当 context 不为空且不是 "default" 时才需要额外更新事件类型聚合视图
        // 避免重复更新同一个 key
        if (!string.IsNullOrEmpty(context) && !string.Equals(context, "default", StringComparison.OrdinalIgnoreCase))
        {
            _eventCounts.AddOrUpdate(eventType, 1, static (k, v) => v + 1);
        }

        _processingTimes.AddOrUpdate(key, processingTime, (k, v) => CalculateEma(v, processingTime));
    }

    /// <summary>
    /// 记录事件冒泡。
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="bubbleDepth">冒泡深度</param>
    /// <param name="context">上下文</param>
    public static void RecordEventBubbled(string eventType, int bubbleDepth, string? context = null)
    {
        var key = GetOrCreateKey(eventType, context);
        var bubbledKey = GetOrCreateKey(key, "Bubbled");
        var eventBubbledKey = GetOrCreateKey(eventType, "Bubbled");

        _eventCounts.AddOrUpdate(bubbledKey, 1, static (k, v) => v + 1);
        _eventCounts.AddOrUpdate(eventBubbledKey, 1, static (k, v) => v + 1);
    }

    /// <summary>
    /// 记录事件过滤。
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="filterName">过滤器名称</param>
    /// <param name="context">上下文</param>
    public static void RecordEventFiltered(string eventType, string filterName, string? context = null)
    {
        var key = GetOrCreateKey3(eventType, filterName, context);

        _filteredEvents.AddOrUpdate(key, 1, static (k, v) => v + 1);
    }

    /// <summary>
    /// 记录订阅变更。
    /// </summary>
    /// <param name="subscriptionType">订阅类型</param>
    /// <param name="isAdded">是否添加</param>
    /// <param name="context">上下文</param>
    public static void RecordSubscriptionChanged(string subscriptionType, bool isAdded, string? context = null)
    {
        var key = GetOrCreateKey(subscriptionType, context);

        if (isAdded)
        {
            _activeSubscriptions.AddOrUpdate(key, 1, static (k, v) => v + 1);
        }
        else
        {
            _activeSubscriptions.AddOrUpdate(key, 0, static (k, v) => Math.Max(0, v - 1));
        }
    }

    /// <summary>
    /// 记录节点操作。
    /// </summary>
    /// <param name="operation">操作类型</param>
    /// <param name="nodeType">节点类型</param>
    /// <param name="processingTime">处理时间</param>
    /// <param name="context">上下文</param>
    public static void RecordNodeOperation(string operation, string nodeType, TimeSpan processingTime, string? context = null)
    {
        var key = GetOrCreateKey3(operation, nodeType, context);

        _eventCounts.AddOrUpdate(key, 1, static (k, v) => v + 1);
        _processingTimes.AddOrUpdate(key, processingTime, (k, v) => CalculateEma(v, processingTime));
    }

    /// <summary>
    /// 获取性能指标。
    /// </summary>
    /// <returns>性能指标</returns>
    public static ChangeBubblingPerformanceMetrics GetMetrics()
    {
        return new ChangeBubblingPerformanceMetrics
        {
            Timestamp = DateTime.UtcNow,
            TotalEvents = SumLongValues(_eventCounts),
            TotalFilteredEvents = SumLongValues(_filteredEvents),
            TotalActiveSubscriptions = SumIntValues(_activeSubscriptions),
            AverageProcessingTime = CalculateAverageProcessingTime(),
            EventCounts = new Dictionary<string, long>(_eventCounts),
            ProcessingTimes = new Dictionary<string, TimeSpan>(_processingTimes),
            ActiveSubscriptions = new Dictionary<string, int>(_activeSubscriptions),
            FilteredEvents = new Dictionary<string, long>(_filteredEvents)
        };
    }

    /// <summary>
    /// 求和 long 值（避免 LINQ 分配）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long SumLongValues(ConcurrentDictionary<string, long> dict)
    {
        long sum = 0;
        foreach (var kvp in dict)
            sum += kvp.Value;
        return sum;
    }

    /// <summary>
    /// 求和 int 值（避免 LINQ 分配）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int SumIntValues(ConcurrentDictionary<string, int> dict)
    {
        int sum = 0;
        foreach (var kvp in dict)
            sum += kvp.Value;
        return sum;
    }

    /// <summary>
    /// 获取事件类型统计。
    /// </summary>
    /// <returns>事件类型统计</returns>
    public static Dictionary<string, EventTypeStatistics> GetEventTypeStatistics()
    {
        var statistics = new Dictionary<string, EventTypeStatistics>();

        foreach (var kvp in _eventCounts)
        {
            var eventType = ExtractEventType(kvp.Key);
            if (!statistics.TryGetValue(eventType, out var stat))
            {
                stat = new EventTypeStatistics
                {
                    EventType = eventType,
                    TotalCount = 0,
                    AverageProcessingTime = TimeSpan.Zero,
                    ActiveSubscriptions = 0,
                    FilteredCount = 0
                };
                statistics[eventType] = stat;
            }

            stat.TotalCount += kvp.Value;
        }

        foreach (var kvp in _processingTimes)
        {
            var eventType = ExtractEventType(kvp.Key);
            if (statistics.TryGetValue(eventType, out var stat))
            {
                stat.AverageProcessingTime = kvp.Value;
            }
        }

        foreach (var kvp in _activeSubscriptions)
        {
            var eventType = ExtractEventType(kvp.Key);
            if (statistics.TryGetValue(eventType, out var stat))
            {
                stat.ActiveSubscriptions += kvp.Value;
            }
        }

        foreach (var kvp in _filteredEvents)
        {
            var eventType = ExtractEventType(kvp.Key);
            if (statistics.TryGetValue(eventType, out var stat))
            {
                stat.FilteredCount += kvp.Value;
            }
        }

        return statistics;
    }

    /// <summary>
    /// 重置指标。
    /// </summary>
    public static void Reset()
    {
        _eventCounts.Clear();
        _processingTimes.Clear();
        _activeSubscriptions.Clear();
        _filteredEvents.Clear();
        _keyCache.Clear();
        _key3Cache.Clear();
        Volatile.Write(ref _keyCacheCount, 0);
        Volatile.Write(ref _key3CacheCount, 0);
    }

    /// <summary>
    /// 计算平均处理时间（避免 LINQ 分配）。
    /// </summary>
    /// <returns>平均处理时间</returns>
    private static TimeSpan CalculateAverageProcessingTime()
    {
        if (_processingTimes.IsEmpty)
        {
            return TimeSpan.Zero;
        }

        double totalMs = 0;
        int count = 0;
        foreach (var kvp in _processingTimes)
        {
            totalMs += kvp.Value.TotalMilliseconds;
            count++;
        }

        return count > 0 ? TimeSpan.FromMilliseconds(totalMs / count) : TimeSpan.Zero;
    }

    /// <summary>
    /// 提取事件类型。
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>事件类型</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ExtractEventType(string key)
    {
        var colonIndex = key.IndexOf(':');
        return colonIndex > 0 ? key[..colonIndex] : key;
    }
}

/// <summary>
/// ChangeBubbling性能指标。
/// </summary>
public class ChangeBubblingPerformanceMetrics
{
    /// <summary>
    /// 时间戳。
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// 总事件数。
    /// </summary>
    public long TotalEvents { get; set; }

    /// <summary>
    /// 总过滤事件数。
    /// </summary>
    public long TotalFilteredEvents { get; set; }

    /// <summary>
    /// 总活跃订阅数。
    /// </summary>
    public int TotalActiveSubscriptions { get; set; }

    /// <summary>
    /// 平均处理时间。
    /// </summary>
    public TimeSpan AverageProcessingTime { get; set; }

    /// <summary>
    /// 事件计数。
    /// </summary>
    public Dictionary<string, long> EventCounts { get; set; } = new();

    /// <summary>
    /// 处理时间。
    /// </summary>
    public Dictionary<string, TimeSpan> ProcessingTimes { get; set; } = new();

    /// <summary>
    /// 活跃订阅。
    /// </summary>
    public Dictionary<string, int> ActiveSubscriptions { get; set; } = new();

    /// <summary>
    /// 过滤事件。
    /// </summary>
    public Dictionary<string, long> FilteredEvents { get; set; } = new();
}

/// <summary>
/// 事件类型统计。
/// </summary>
public class EventTypeStatistics
{
    /// <summary>
    /// 事件类型。
    /// </summary>
    public string EventType { get; set; } = string.Empty;

    /// <summary>
    /// 总计数。
    /// </summary>
    public long TotalCount { get; set; }

    /// <summary>
    /// 平均处理时间。
    /// </summary>
    public TimeSpan AverageProcessingTime { get; set; }

    /// <summary>
    /// 活跃订阅数。
    /// </summary>
    public int ActiveSubscriptions { get; set; }

    /// <summary>
    /// 过滤计数。
    /// </summary>
    public long FilteredCount { get; set; }
}
