using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Apq.ChangeBubbling.Abstractions;

namespace Apq.ChangeBubbling.Core;

/// <summary>
/// 弱事件订阅管理器，避免父节点持有子节点的强引用导致内存泄漏。
/// 使用缓存的强引用优化高频事件处理性能。
/// 检查间隔使用自适应策略：高频事件时增加间隔，低频时减少间隔。
/// </summary>
internal sealed class WeakEventSubscription : IDisposable
{
    private readonly WeakReference<IChangeNode> _sourceRef;
    private readonly WeakReference<ChangeNodeBase> _targetRef;

    // 缓存强引用，减少 TryGetTarget 调用开销
    // 在正常情况下目标不会被回收，缓存可以显著提升性能
    private volatile ChangeNodeBase? _cachedTarget;
    private int _checkCounter;

    // 自适应检查间隔配置
    private const int MinCheckInterval = 50;      // 最小检查间隔
    private const int MaxCheckInterval = 1000;    // 最大检查间隔
    private const int InitialCheckInterval = 100; // 初始检查间隔
    private int _currentCheckInterval = InitialCheckInterval;

    // 用于自适应调整的计数器
    private int _consecutiveHits;    // 连续命中缓存次数
    private int _consecutiveMisses;  // 连续未命中次数

    private int _disposed; // 0 = not disposed, 1 = disposed (使用 int 配合 Interlocked)

    public WeakEventSubscription(IChangeNode source, ChangeNodeBase target)
    {
        _sourceRef = new WeakReference<IChangeNode>(source);
        _targetRef = new WeakReference<ChangeNodeBase>(target);
        _cachedTarget = target;

        source.PropertyChanged += OnPropertyChanged;
        source.NodeChanged += OnNodeChanged;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ChangeNodeBase? GetTarget()
    {
        // 快速路径：使用缓存的强引用
        var cached = _cachedTarget;
        if (cached is not null)
        {
            // 定期检查弱引用是否仍然有效（使用原子递增）
            var currentInterval = Volatile.Read(ref _currentCheckInterval);
            if (Interlocked.Increment(ref _checkCounter) < currentInterval)
                return cached;

            // 重置计数器（允许轻微的竞态，不影响正确性）
            Interlocked.Exchange(ref _checkCounter, 0);

            // 验证缓存仍然有效
            if (_targetRef.TryGetTarget(out var target) && ReferenceEquals(target, cached))
            {
                // 缓存命中，考虑增加检查间隔（减少检查频率）
                AdaptIntervalOnHit();
                return cached;
            }

            // 缓存失效，清除
            _cachedTarget = null;
            AdaptIntervalOnMiss();
        }

        // 慢速路径：从弱引用获取
        if (_targetRef.TryGetTarget(out var newTarget))
        {
            _cachedTarget = newTarget;
            return newTarget;
        }

        return null;
    }

    /// <summary>
    /// 缓存命中时调整间隔（增加间隔，减少检查频率）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AdaptIntervalOnHit()
    {
        _consecutiveMisses = 0;
        var hits = Interlocked.Increment(ref _consecutiveHits);

        // 每连续命中 10 次，增加检查间隔
        if (hits >= 10)
        {
            Interlocked.Exchange(ref _consecutiveHits, 0);
            var current = Volatile.Read(ref _currentCheckInterval);
            if (current < MaxCheckInterval)
            {
                // 增加 50%，但不超过最大值
                var newInterval = Math.Min(current + current / 2, MaxCheckInterval);
                Volatile.Write(ref _currentCheckInterval, newInterval);
            }
        }
    }

    /// <summary>
    /// 缓存未命中时调整间隔（减少间隔，增加检查频率）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AdaptIntervalOnMiss()
    {
        _consecutiveHits = 0;
        var misses = Interlocked.Increment(ref _consecutiveMisses);

        // 每未命中 3 次，减少检查间隔
        if (misses >= 3)
        {
            Interlocked.Exchange(ref _consecutiveMisses, 0);
            var current = Volatile.Read(ref _currentCheckInterval);
            if (current > MinCheckInterval)
            {
                // 减少 50%，但不低于最小值
                var newInterval = Math.Max(current / 2, MinCheckInterval);
                Volatile.Write(ref _currentCheckInterval, newInterval);
            }
        }
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_disposed != 0) return;

        var target = GetTarget();
        if (target is null)
        {
            // 目标已被回收，自动清理
            Dispose();
            return;
        }

        target.HandleChildPropertyChangedInternal(sender, e);
    }

    private void OnNodeChanged(object? sender, BubblingChange e)
    {
        if (_disposed != 0) return;

        var target = GetTarget();
        if (target is null)
        {
            // 目标已被回收，自动清理
            Dispose();
            return;
        }

        target.HandleChildNodeChangedInternal(sender, e);
    }

    public void Dispose()
    {
        // 使用 Interlocked 确保只执行一次
        if (Interlocked.Exchange(ref _disposed, 1) != 0) return;
        _cachedTarget = null;

        if (_sourceRef.TryGetTarget(out var source))
        {
            source.PropertyChanged -= OnPropertyChanged;
            source.NodeChanged -= OnNodeChanged;
        }
    }
}
