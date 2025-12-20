using System;
using System.Collections.Generic;
using Apq.ChangeBubbling.Abstractions;

namespace Apq.ChangeBubbling.Infrastructure.EventFiltering;

/// <summary>
/// 变更事件过滤器接口，提供事件过滤能力。
/// </summary>
public interface IChangeEventFilter
{
    /// <summary>
    /// 判断是否应该处理事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该处理</returns>
    bool ShouldProcess(BubblingChange change, string? context = null);

    /// <summary>
    /// 判断是否应该冒泡事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该冒泡</returns>
    bool ShouldBubble(BubblingChange change, string? context = null);

    /// <summary>
    /// 过滤器名称。
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 过滤器描述。
    /// </summary>
    string? Description { get; }
}

/// <summary>
/// 基于属性的变更事件过滤器。
/// </summary>
public class PropertyBasedEventFilter : IChangeEventFilter
{
    private readonly string[] _allowedProperties;
    private readonly string[] _excludedProperties;
    private readonly NodeChangeKind[] _allowedKinds;
    private readonly NodeChangeKind[] _excludedKinds;

    // 精确匹配时使用 HashSet 加速查找
    private readonly HashSet<string>? _allowedPropertiesSet;
    private readonly HashSet<string>? _excludedPropertiesSet;
    private readonly HashSet<NodeChangeKind>? _allowedKindsSet;
    private readonly HashSet<NodeChangeKind>? _excludedKindsSet;

    private readonly bool _useExactMatch;

    /// <summary>
    /// 创建基于属性的过滤器。
    /// </summary>
    /// <param name="allowedProperties">允许的属性</param>
    /// <param name="excludedProperties">排除的属性</param>
    /// <param name="allowedKinds">允许的变更类型</param>
    /// <param name="excludedKinds">排除的变更类型</param>
    /// <param name="useExactMatch">是否使用精确匹配（默认 false 为模糊匹配）</param>
    public PropertyBasedEventFilter(
        string[]? allowedProperties = null,
        string[]? excludedProperties = null,
        NodeChangeKind[]? allowedKinds = null,
        NodeChangeKind[]? excludedKinds = null,
        bool useExactMatch = false)
    {
        _allowedProperties = allowedProperties ?? Array.Empty<string>();
        _excludedProperties = excludedProperties ?? Array.Empty<string>();
        _allowedKinds = allowedKinds ?? Array.Empty<NodeChangeKind>();
        _excludedKinds = excludedKinds ?? Array.Empty<NodeChangeKind>();
        _useExactMatch = useExactMatch;

        // 精确匹配模式下使用 HashSet 加速
        if (useExactMatch)
        {
            if (_allowedProperties.Length > 0)
                _allowedPropertiesSet = new HashSet<string>(_allowedProperties, StringComparer.OrdinalIgnoreCase);
            if (_excludedProperties.Length > 0)
                _excludedPropertiesSet = new HashSet<string>(_excludedProperties, StringComparer.OrdinalIgnoreCase);
        }

        // NodeChangeKind 总是精确匹配，超过 3 个时使用 HashSet
        if (_allowedKinds.Length > 3)
            _allowedKindsSet = new HashSet<NodeChangeKind>(_allowedKinds);
        if (_excludedKinds.Length > 3)
            _excludedKindsSet = new HashSet<NodeChangeKind>(_excludedKinds);
    }

    /// <summary>
    /// 过滤器名称。
    /// </summary>
    public string Name => "PropertyBasedEventFilter";

    /// <summary>
    /// 过滤器描述。
    /// </summary>
    public string? Description => "基于属性和变更类型的过滤器";

    /// <summary>
    /// 判断是否应该处理事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该处理</returns>
    public bool ShouldProcess(BubblingChange change, string? context = null)
    {
        // 检查属性过滤
        if (!IsPropertyAllowed(change.PropertyName))
        {
            return false;
        }

        // 检查变更类型过滤
        if (!IsKindAllowed(change.Kind))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 判断是否应该冒泡事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该冒泡</returns>
    public bool ShouldBubble(BubblingChange change, string? context = null)
    {
        return ShouldProcess(change, context);
    }

    /// <summary>
    /// 检查属性是否被允许。
    /// </summary>
    /// <param name="propertyName">属性名</param>
    /// <returns>是否被允许</returns>
    private bool IsPropertyAllowed(string propertyName)
    {
        // 精确匹配模式：使用 HashSet O(1) 查找
        if (_useExactMatch)
        {
            if (_excludedPropertiesSet is not null && _excludedPropertiesSet.Contains(propertyName))
                return false;

            if (_allowedPropertiesSet is not null)
                return _allowedPropertiesSet.Contains(propertyName);

            return true;
        }

        // 模糊匹配模式：遍历检查 Contains
        if (_excludedProperties.Length > 0)
        {
            foreach (var excluded in _excludedProperties)
            {
                if (propertyName.Contains(excluded, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
        }

        if (_allowedProperties.Length > 0)
        {
            foreach (var allowed in _allowedProperties)
            {
                if (propertyName.Contains(allowed, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        return true;
    }

    /// <summary>
    /// 检查变更类型是否被允许。
    /// </summary>
    /// <param name="kind">变更类型</param>
    /// <returns>是否被允许</returns>
    private bool IsKindAllowed(NodeChangeKind kind)
    {
        // 使用 HashSet 快速路径（如果可用）
        if (_excludedKindsSet is not null)
        {
            if (_excludedKindsSet.Contains(kind))
                return false;
        }
        else if (_excludedKinds.Length > 0)
        {
            foreach (var excluded in _excludedKinds)
            {
                if (kind == excluded)
                    return false;
            }
        }

        if (_allowedKindsSet is not null)
            return _allowedKindsSet.Contains(kind);

        if (_allowedKinds.Length > 0)
        {
            foreach (var allowed in _allowedKinds)
            {
                if (kind == allowed)
                    return true;
            }
            return false;
        }

        return true;
    }
}

/// <summary>
/// 基于路径的变更事件过滤器。
/// </summary>
public class PathBasedEventFilter : IChangeEventFilter
{
    private readonly string[] _allowedPaths;
    private readonly string[] _excludedPaths;
    private readonly int _maxDepth;

    /// <summary>
    /// 创建基于路径的过滤器。
    /// </summary>
    /// <param name="allowedPaths">允许的路径</param>
    /// <param name="excludedPaths">排除的路径</param>
    /// <param name="maxDepth">最大深度</param>
    public PathBasedEventFilter(
        string[]? allowedPaths = null,
        string[]? excludedPaths = null,
        int maxDepth = int.MaxValue)
    {
        _allowedPaths = allowedPaths ?? Array.Empty<string>();
        _excludedPaths = excludedPaths ?? Array.Empty<string>();
        _maxDepth = maxDepth;
    }

    /// <summary>
    /// 过滤器名称。
    /// </summary>
    public string Name => "PathBasedEventFilter";

    /// <summary>
    /// 过滤器描述。
    /// </summary>
    public string? Description => "基于路径和深度的过滤器";

    /// <summary>
    /// 判断是否应该处理事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该处理</returns>
    public bool ShouldProcess(BubblingChange change, string? context = null)
    {
        // 检查路径深度
        if (change.PathSegments == null || change.PathSegments.Count == 0)
        {
            // 根路径，当未设置路径过滤时视为通过
            if (_allowedPaths.Length == 0)
            {
                return true;
            }
        }
        if (change.PathSegments?.Count > _maxDepth)
        {
            return false;
        }

        // 检查路径过滤
        return IsPathAllowed(change.PathSegments ?? Array.Empty<string>());
    }

    /// <summary>
    /// 判断是否应该冒泡事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该冒泡</returns>
    public bool ShouldBubble(BubblingChange change, string? context = null)
    {
        return ShouldProcess(change, context);
    }

    /// <summary>
    /// 检查路径是否被允许（避免 string.Join 分配）。
    /// </summary>
    /// <param name="pathSegments">路径段</param>
    /// <returns>是否被允许</returns>
    private bool IsPathAllowed(IReadOnlyList<string> pathSegments)
    {
        // 如果指定了排除路径，检查是否在排除列表中
        if (_excludedPaths.Length > 0)
        {
            foreach (var excluded in _excludedPaths)
            {
                if (MatchesPath(pathSegments, excluded))
                {
                    return false;
                }
            }
        }

        // 如果指定了允许路径，检查是否在允许列表中
        if (_allowedPaths.Length > 0)
        {
            // 若未提供路径，则不视为通过（需要显式匹配到允许路径）
            if (pathSegments is null || pathSegments.Count == 0) return false;
            foreach (var allowed in _allowedPaths)
            {
                if (MatchesPath(pathSegments, allowed))
                {
                    return true;
                }
            }
            return false;
        }

        return true;
    }

    /// <summary>
    /// 检查路径段是否匹配指定模式（避免 string.Join）。
    /// </summary>
    private static bool MatchesPath(IReadOnlyList<string>? pathSegments, string pattern)
    {
        if (pathSegments is null || pathSegments.Count == 0)
            return string.IsNullOrEmpty(pattern);

        // 快速路径：如果模式不包含分隔符，直接检查任一段是否包含
        if (!pattern.Contains('.'))
        {
            foreach (var segment in pathSegments)
            {
                if (segment.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        // 慢速路径：需要完整路径匹配时才拼接
        var fullPath = string.Join(".", pathSegments);
        return fullPath.Contains(pattern, StringComparison.OrdinalIgnoreCase);
    }
}

/// <summary>
/// 基于频率的变更事件过滤器（线程安全）。
/// 自动清理过期条目，防止内存泄漏。
/// </summary>
public class FrequencyBasedEventFilter : IChangeEventFilter
{
    private readonly TimeSpan _throttleInterval;
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, DateTime> _lastProcessedTimes = new();

    // 清理相关配置
    private readonly int _maxEntries;
    private readonly TimeSpan _cleanupThreshold;
    private int _operationCount;
    private const int CleanupCheckInterval = 1000; // 每 1000 次操作检查一次

    // 字符串缓存，避免高频场景下重复拼接
    private readonly System.Collections.Concurrent.ConcurrentDictionary<(string, string?), string> _keyCache = new();
    private const int MaxKeyCacheSize = 10000;
    private int _keyCacheCount;

    /// <summary>
    /// 创建基于频率的过滤器。
    /// </summary>
    /// <param name="throttleInterval">节流间隔</param>
    /// <param name="maxEntries">最大条目数（默认 10000）</param>
    /// <param name="cleanupThreshold">过期清理阈值（默认为节流间隔的 10 倍）</param>
    public FrequencyBasedEventFilter(
        TimeSpan throttleInterval,
        int maxEntries = 10000,
        TimeSpan? cleanupThreshold = null)
    {
        _throttleInterval = throttleInterval;
        _maxEntries = maxEntries;
        _cleanupThreshold = cleanupThreshold ?? TimeSpan.FromTicks(throttleInterval.Ticks * 10);
    }

    /// <summary>
    /// 过滤器名称。
    /// </summary>
    public string Name => "FrequencyBasedEventFilter";

    /// <summary>
    /// 过滤器描述。
    /// </summary>
    public string? Description => $"基于频率的过滤器，节流间隔: {_throttleInterval}";

    /// <summary>
    /// 判断是否应该处理事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该处理</returns>
    public bool ShouldProcess(BubblingChange change, string? context = null)
    {
        var key = GetOrCreateKey(change.PropertyName, context);
        var now = DateTime.UtcNow;

        // 定期检查是否需要清理
        if (System.Threading.Interlocked.Increment(ref _operationCount) % CleanupCheckInterval == 0)
        {
            TryCleanupExpiredEntries(now);
        }

        // 使用 AddOrUpdate 原子操作，线程安全
        var shouldProcess = false;
        _lastProcessedTimes.AddOrUpdate(
            key,
            _ =>
            {
                shouldProcess = true;
                return now;
            },
            (_, lastTime) =>
            {
                if (now - lastTime >= _throttleInterval)
                {
                    shouldProcess = true;
                    return now;
                }
                return lastTime;
            });

        return shouldProcess;
    }

    /// <summary>
    /// 获取或创建缓存的 key。
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private string GetOrCreateKey(string propertyName, string? context)
    {
        var cacheKey = (propertyName, context);
        if (_keyCache.TryGetValue(cacheKey, out var cached))
            return cached;

        var key = $"{propertyName}:{context}";

        // 检查容量限制
        if (System.Threading.Volatile.Read(ref _keyCacheCount) >= MaxKeyCacheSize)
            return key;

        if (_keyCache.TryAdd(cacheKey, key))
        {
            System.Threading.Interlocked.Increment(ref _keyCacheCount);
        }
        return key;
    }

    /// <summary>
    /// 判断是否应该冒泡事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该冒泡</returns>
    public bool ShouldBubble(BubblingChange change, string? context = null)
    {
        return ShouldProcess(change, context);
    }

    /// <summary>
    /// 尝试清理过期条目（直接遍历删除，避免临时 List 分配）。
    /// </summary>
    private void TryCleanupExpiredEntries(DateTime now)
    {
        // 如果条目数未超过限制，不清理
        if (_lastProcessedTimes.Count <= _maxEntries)
            return;

        // 直接遍历删除过期条目（ConcurrentDictionary 支持遍历时删除）
        foreach (var kvp in _lastProcessedTimes)
        {
            if (now - kvp.Value > _cleanupThreshold)
            {
                _lastProcessedTimes.TryRemove(kvp.Key, out _);
            }
        }
    }

    /// <summary>
    /// 手动清理所有过期条目。
    /// </summary>
    public void CleanupExpiredEntries()
    {
        TryCleanupExpiredEntries(DateTime.UtcNow);
    }

    /// <summary>
    /// 重置过滤器状态。
    /// </summary>
    public void Reset()
    {
        _lastProcessedTimes.Clear();
        _keyCache.Clear();
        _operationCount = 0;
        System.Threading.Volatile.Write(ref _keyCacheCount, 0);
    }
}

/// <summary>
/// 组合变更事件过滤器。
/// </summary>
public class CompositeEventFilter : IChangeEventFilter
{
    private readonly IChangeEventFilter[] _filters;
    private readonly FilterMode _mode;

    /// <summary>
    /// 过滤器模式。
    /// </summary>
    public enum FilterMode
    {
        /// <summary>
        /// 所有过滤器都必须通过。
        /// </summary>
        All,
        /// <summary>
        /// 任一过滤器通过即可。
        /// </summary>
        Any
    }

    /// <summary>
    /// 创建组合过滤器。
    /// </summary>
    /// <param name="filters">过滤器列表</param>
    /// <param name="mode">过滤模式</param>
    public CompositeEventFilter(IChangeEventFilter[] filters, FilterMode mode = FilterMode.All)
    {
        _filters = filters ?? throw new ArgumentNullException(nameof(filters));
        _mode = mode;
    }

    /// <summary>
    /// 过滤器名称。
    /// </summary>
    public string Name => "CompositeEventFilter";

    /// <summary>
    /// 过滤器描述。
    /// </summary>
    public string? Description => $"组合过滤器，模式: {_mode}";

    /// <summary>
    /// 判断是否应该处理事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该处理</returns>
    public bool ShouldProcess(BubblingChange change, string? context = null)
    {
        if (_mode == FilterMode.All)
        {
            foreach (var filter in _filters)
            {
                var ok = filter.ShouldProcess(change, context);
                if (!ok)
                {
                    // 特判：All 模式下，路径过滤器在无路径信息时不作为阻塞条件
                    if (filter is PathBasedEventFilter && (change.PathSegments == null || change.PathSegments.Count == 0))
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }
        else
        {
            // Any 模式：任一过滤器通过即可；若无过滤器则返回 false
            var any = false;
            foreach (var filter in _filters)
            {
                var ok = filter.ShouldProcess(change, context);
                if (!ok)
                {
                    // 特判：Any 模式下，路径过滤器在无路径信息时不计为通过
                    continue;
                }
                else
                {
                    any = true;
                    break;
                }
            }
            return any;
        }
    }

    /// <summary>
    /// 判断是否应该冒泡事件。
    /// </summary>
    /// <param name="change">变更事件</param>
    /// <param name="context">上下文</param>
    /// <returns>是否应该冒泡</returns>
    public bool ShouldBubble(BubblingChange change, string? context = null)
    {
        if (_mode == FilterMode.All)
        {
            foreach (var filter in _filters)
            {
                var ok = filter.ShouldBubble(change, context);
                if (!ok)
                {
                    if (filter is PathBasedEventFilter && (change.PathSegments == null || change.PathSegments.Count == 0))
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }
        else
        {
            var any = false;
            foreach (var filter in _filters)
            {
                var ok = filter.ShouldBubble(change, context);
                if (!ok)
                {
                    continue;
                }
                else
                {
                    any = true;
                    break;
                }
            }
            return any;
        }
    }
}
