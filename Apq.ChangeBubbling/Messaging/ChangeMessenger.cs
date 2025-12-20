using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Infrastructure.Compatibility;
using Apq.ChangeBubbling.Infrastructure.EventFiltering;
using Apq.ChangeBubbling.Infrastructure.Performance;
using CommunityToolkit.Mvvm.Messaging;

namespace Apq.ChangeBubbling.Messaging;

/// <summary>
/// 冒泡事件消息中心：弱引用消息 + Rx 事件流 + 可插拔调度环境。
/// </summary>
public static class ChangeMessenger
{
    private sealed record SchedulerPair(IScheduler Publish, IScheduler Observe);

    // 用于避免 Lambda 闭包的状态结构
    private readonly record struct PublishState(Subject<BubblingChange> Stream, BubblingChange Change);

    // 缓存的静态委托，避免每次调度都创建新委托
    private static readonly Action<PublishState> _publishAction = static state => state.Stream.OnNext(state.Change);

    private static readonly ConcurrentDictionary<string, Subject<BubblingChange>> _streams = new();
    private static readonly ConcurrentDictionary<string, SchedulerPair> _env = new();
    private static readonly ConcurrentDictionary<string, IChangeEventFilter> _filters = new();
    private static readonly ConcurrentDictionary<string, Func<Action, System.Threading.Tasks.Task>> _customPublish = new();

    private const string DefaultEnv = "default";

    // 缓存默认环境的常用对象，避免重复查找
    private static Subject<BubblingChange>? _defaultStream;
    private static SchedulerPair? _defaultSchedulers;
    private static IChangeEventFilter? _defaultFilter;
    private static bool _hasDefaultFilter;

    /// <summary>
    /// 是否启用性能指标收集。高性能场景下可禁用以减少开销。
    /// </summary>
    public static bool EnableMetrics { get; set; } = true;

    /// <summary>
    /// 是否启用 WeakReferenceMessenger 发布。如果只使用 Rx 流可禁用。
    /// </summary>
    public static bool EnableWeakMessenger { get; set; } = true;

    /// <summary>
    /// 是否启用 Rx Subject 发布。如果只使用 WeakReferenceMessenger 可禁用。
    /// </summary>
    public static bool EnableRxStream { get; set; } = true;

    /// <summary>
    /// 是否使用同步发布模式（直接调用 OnNext，不经过调度器）。
    /// 启用后可减少 Lambda 闭包分配和调度延迟，但会在调用线程上执行。
    /// 适用于不需要线程切换的高性能场景。
    /// </summary>
    public static bool UseSynchronousPublish { get; set; } = false;

    static ChangeMessenger()
    {
        RegisterThreadPool(DefaultEnv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsDefaultEnv(string name) => ReferenceEquals(name, DefaultEnv) || string.Equals(name, DefaultEnv, StringComparison.Ordinal);

    private static Subject<BubblingChange> GetStream(string name)
    {
        // 快速路径：默认环境使用缓存
        if (IsDefaultEnv(name) && _defaultStream is not null)
            return _defaultStream;

        var stream = _streams.GetOrAdd(name, _ => new Subject<BubblingChange>());

        if (IsDefaultEnv(name))
            _defaultStream = stream;

        return stream;
    }

    private static SchedulerPair GetSchedulers(string name)
    {
        // 快速路径：默认环境使用缓存
        if (IsDefaultEnv(name) && _defaultSchedulers is not null)
            return _defaultSchedulers;

        if (_env.TryGetValue(name, out var sp))
        {
            if (IsDefaultEnv(name))
                _defaultSchedulers = sp;
            return sp;
        }

        return _env[DefaultEnv];
    }

    /// <summary>
    /// 注册线程池环境。
    /// </summary>
    public static void RegisterThreadPool(string name)
    {
        var pair = new SchedulerPair(TaskPoolScheduler.Default, TaskPoolScheduler.Default);
        _env[name] = pair;
        _customPublish.TryRemove(name, out _);

        if (IsDefaultEnv(name))
            _defaultSchedulers = pair;
    }

    /// <summary>
    /// 注册基于当前 SynchronizationContext 的环境。需在目标线程调用。
    /// </summary>
    public static void RegisterDispatcher(string name)
    {
        var ctx = SynchronizationContext.Current;
        if (ctx is null)
        {
            RegisterThreadPool(name);
            return;
        }
        var sc = new SynchronizationContextScheduler(ctx);
        var pair = new SchedulerPair(sc, sc);
        _env[name] = pair;
        _customPublish.TryRemove(name, out _);

        if (IsDefaultEnv(name))
            _defaultSchedulers = pair;
    }

    /// <summary>
    /// 注册专用线程事件循环环境（串行执行）。返回可释放的调度器实例。
    /// </summary>
    public static IDisposable RegisterDedicatedThread(string name, string? threadName = null)
    {
        var loop = new EventLoopScheduler(ts =>
        {
            var t = new Thread(ts) { IsBackground = true };
            if (!string.IsNullOrEmpty(threadName)) t.Name = threadName;
            return t;
        });
        var pair = new SchedulerPair(loop, loop);
        _env[name] = pair;
        _customPublish.TryRemove(name, out _);

        if (IsDefaultEnv(name))
            _defaultSchedulers = pair;

        return loop;
    }

    /// <summary>
    /// 注册基于 Nito.AsyncEx 的 AsyncContext 线程环境（作为发布执行器）。
    /// 观察侧仍可使用线程池或调用方自定义的 Observe 调度。
    /// </summary>
    public static void RegisterNitoAsyncContext(string name, Func<System.Threading.Tasks.Task> warmup, Func<System.Action, System.Threading.Tasks.Task> run)
    {
        // 保持与现有模型兼容：发布使用自定义执行器，观察使用线程池
        var pair = new SchedulerPair(TaskPoolScheduler.Default, TaskPoolScheduler.Default);
        _env[name] = pair;
        _customPublish[name] = run;

        if (IsDefaultEnv(name))
            _defaultSchedulers = pair;

        // 可选预热：确保线程/上下文已建立
        _ = warmup();
    }

    /// <summary>
    /// 发布冒泡事件到指定环境（未注册则落到默认）。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Publish(BubblingChange change, string envName = DefaultEnv)
    {
        // 默认环境快速路径：跳过大部分字典查找
        if (IsDefaultEnv(envName))
        {
            PublishToDefaultEnv(change);
            return;
        }

        PublishToNamedEnv(change, envName);
    }

    /// <summary>
    /// 默认环境的快速发布路径。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void PublishToDefaultEnv(BubblingChange change)
    {
        long startTimestamp = EnableMetrics ? Stopwatch.GetTimestamp() : 0;

        try
        {
            // 检查默认环境过滤器（使用缓存）
            if (_hasDefaultFilter && _defaultFilter is not null)
            {
                if (!_defaultFilter.ShouldProcess(change, DefaultEnv))
                {
                    if (EnableMetrics)
                        ChangeBubblingMetrics.RecordEventFiltered(change.PropertyName, _defaultFilter.Name, DefaultEnv);
                    return;
                }
            }

            // 发布到 WeakReferenceMessenger
            if (EnableWeakMessenger)
            {
                var msg = BubblingChangeMessage.Rent(change);
                WeakReferenceMessenger.Default.Send(msg);
                msg.Return();
            }

            // 发布到 Rx Subject（默认环境不使用 customPublish）
            if (EnableRxStream)
            {
                var stream = _defaultStream ?? GetStream(DefaultEnv);

                if (UseSynchronousPublish)
                {
                    // 同步发布：直接调用，避免 Lambda 闭包分配
                    stream.OnNext(change);
                }
                else
                {
                    // 异步发布：通过调度器（使用状态参数避免闭包分配）
                    var sched = _defaultSchedulers ?? GetSchedulers(DefaultEnv);
                    var state = new PublishState(stream, change);
                    sched.Publish.Schedule(state, static (scheduler, s) =>
                    {
                        _publishAction(s);
                        return System.Reactive.Disposables.Disposable.Empty;
                    });
                }
            }

            // 记录性能指标
            if (EnableMetrics)
            {
#if NET7_0_OR_GREATER
                var elapsed = Stopwatch.GetElapsedTime(startTimestamp);
#else
                var elapsed = StopwatchExtensions.GetElapsedTime(startTimestamp);
#endif
                ChangeBubblingMetrics.RecordEventProcessed(change.PropertyName, elapsed, DefaultEnv);
            }
        }
        catch (Exception)
        {
            if (EnableMetrics)
            {
                ChangeBubblingMetrics.RecordEventFiltered(change.PropertyName, "Exception", DefaultEnv);
            }
            throw;
        }
    }

    /// <summary>
    /// 命名环境的发布路径。
    /// </summary>
    private static void PublishToNamedEnv(BubblingChange change, string envName)
    {
        long startTimestamp = EnableMetrics ? Stopwatch.GetTimestamp() : 0;

        try
        {
            // 应用事件过滤器
            if (_filters.TryGetValue(envName, out var filter))
            {
                if (!filter.ShouldProcess(change, envName))
                {
                    if (EnableMetrics)
                        ChangeBubblingMetrics.RecordEventFiltered(change.PropertyName, filter.Name, envName);
                    return;
                }
            }

            // 发布到 WeakReferenceMessenger
            if (EnableWeakMessenger)
            {
                var msg = BubblingChangeMessage.Rent(change);
                WeakReferenceMessenger.Default.Send(msg);
                msg.Return();
            }

            // 发布到 Rx Subject
            if (EnableRxStream)
            {
                var stream = GetStream(envName);

                // 提前查找 customPublish，避免重复查找
                var hasCustomRunner = _customPublish.TryGetValue(envName, out var runner);

                if (hasCustomRunner)
                {
                    // customPublish 始终使用异步方式，因为它是专门为异步上下文设计的
                    _ = runner!(() => stream.OnNext(change));
                }
                else if (UseSynchronousPublish)
                {
                    // 同步发布：直接调用，避免 Lambda 闭包分配
                    stream.OnNext(change);
                }
                else
                {
                    // 异步发布：通过调度器（使用状态参数避免闭包分配）
                    var sched = GetSchedulers(envName).Publish;
                    var state = new PublishState(stream, change);
                    sched.Schedule(state, static (scheduler, s) =>
                    {
                        _publishAction(s);
                        return System.Reactive.Disposables.Disposable.Empty;
                    });
                }
            }

            // 记录性能指标
            if (EnableMetrics)
            {
#if NET7_0_OR_GREATER
                var elapsed = Stopwatch.GetElapsedTime(startTimestamp);
#else
                var elapsed = StopwatchExtensions.GetElapsedTime(startTimestamp);
#endif
                ChangeBubblingMetrics.RecordEventProcessed(change.PropertyName, elapsed, envName);
            }
        }
        catch (Exception)
        {
            if (EnableMetrics)
            {
                ChangeBubblingMetrics.RecordEventFiltered(change.PropertyName, "Exception", envName);
            }
            throw;
        }
    }

    /// <summary>
    /// 注册事件过滤器。
    /// </summary>
    /// <param name="envName">环境名称</param>
    /// <param name="filter">事件过滤器</param>
    public static void RegisterFilter(string envName, IChangeEventFilter filter)
    {
        _filters[envName] = filter;

        if (IsDefaultEnv(envName))
        {
            _defaultFilter = filter;
            _hasDefaultFilter = true;
        }
    }

    /// <summary>
    /// 移除事件过滤器。
    /// </summary>
    /// <param name="envName">环境名称</param>
    public static void RemoveFilter(string envName)
    {
        _filters.TryRemove(envName, out _);

        if (IsDefaultEnv(envName))
        {
            _defaultFilter = null;
            _hasDefaultFilter = false;
        }
    }

    /// <summary>
    /// 获取性能指标。
    /// </summary>
    /// <returns>性能指标</returns>
    public static ChangeBubblingPerformanceMetrics GetPerformanceMetrics()
    {
        return ChangeBubblingMetrics.GetMetrics();
    }

    /// <summary>
    /// 获取事件类型统计。
    /// </summary>
    /// <returns>事件类型统计</returns>
    public static Dictionary<string, EventTypeStatistics> GetEventTypeStatistics()
    {
        return ChangeBubblingMetrics.GetEventTypeStatistics();
    }

    /// <summary>
    /// 获取指定环境的原始事件流。
    /// </summary>
    public static IObservable<BubblingChange> AsObservable(string envName = DefaultEnv)
    {
        var stream = GetStream(envName);
        var sched = GetSchedulers(envName).Observe;
        return stream.ObserveOn(sched).AsObservable();
    }

    /// <summary>
    /// 获取指定环境的节流事件流（默认 50ms）。
    /// </summary>
    public static IObservable<BubblingChange> AsThrottledObservable(TimeSpan? dueTime = null, string envName = DefaultEnv)
    {
        var dt = dueTime ?? TimeSpan.FromMilliseconds(50);
        var stream = GetStream(envName);
        var sched = GetSchedulers(envName).Observe;
        return stream.ObserveOn(sched).Throttle(dt);
    }

    /// <summary>
    /// 获取指定环境的缓冲批量事件流。满足时间窗口或数量窗口任一条件即触发。
    /// </summary>
    public static IObservable<IList<BubblingChange>> AsBufferedObservable(TimeSpan timeWindow, int countWindow, string envName = DefaultEnv)
    {
        var stream = GetStream(envName);
        var sched = GetSchedulers(envName).Observe;
        return stream.ObserveOn(sched).Buffer(timeWindow, countWindow);
    }

    /// <summary>
    /// 获取指定环境的时间窗口切片事件流（Window）。
    /// </summary>
    public static IObservable<IObservable<BubblingChange>> AsWindowedObservable(TimeSpan timeWindow, string envName = DefaultEnv)
    {
        var stream = GetStream(envName);
        var sched = GetSchedulers(envName).Observe;
        return stream.ObserveOn(sched).Window(timeWindow);
    }

    /// <summary>
    /// 重置消息中心状态（用于测试）。
    /// 清除所有环境、流、过滤器，并重新注册默认线程池环境。
    /// </summary>
    public static void Reset()
    {
        // 清除所有流
        foreach (var stream in _streams.Values)
        {
            stream.Dispose();
        }
        _streams.Clear();

        // 清除环境和过滤器
        _env.Clear();
        _filters.Clear();
        _customPublish.Clear();

        // 重置缓存
        _defaultStream = null;
        _defaultSchedulers = null;
        _defaultFilter = null;
        _hasDefaultFilter = false;

        // 重新注册默认环境
        RegisterThreadPool(DefaultEnv);
    }
}
