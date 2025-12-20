using System.Buffers;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Apq.ChangeBubbling.Abstractions;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Apq.ChangeBubbling.Messaging;

/// <summary>
/// 基于 CommunityToolkit.Mvvm 的冒泡变更消息类型。
/// 使用对象池减少高频场景下的 GC 压力。
/// </summary>
public sealed class BubblingChangeMessage : ValueChangedMessage<BubblingChange>
{
    private static readonly ObjectPool<BubblingChangeMessage> Pool = new(() => new BubblingChangeMessage());

    // 使用表达式树编译的委托，比反射快 10-100 倍
    private static readonly Action<BubblingChangeMessage, BubblingChange>? ValueSetter;

    static BubblingChangeMessage()
    {
        // 获取基类的私有字段
        var field = typeof(ValueChangedMessage<BubblingChange>)
            .GetField("<Value>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

        if (field is not null)
        {
            // 编译表达式树：(msg, value) => msg.<Value>k__BackingField = value
            var msgParam = Expression.Parameter(typeof(BubblingChangeMessage), "msg");
            var valueParam = Expression.Parameter(typeof(BubblingChange), "value");
            var fieldAccess = Expression.Field(msgParam, field);
            var assign = Expression.Assign(fieldAccess, valueParam);
            ValueSetter = Expression.Lambda<Action<BubblingChangeMessage, BubblingChange>>(assign, msgParam, valueParam).Compile();
        }
    }

    private BubblingChangeMessage() : base(default)
    {
    }

    /// <summary>
    /// 从对象池获取消息实例。
    /// </summary>
    public static BubblingChangeMessage Rent(BubblingChange value)
    {
        var msg = Pool.Get();
        msg.SetValue(value);
        return msg;
    }

    /// <summary>
    /// 归还消息实例到对象池。
    /// </summary>
    public void Return()
    {
        Pool.Return(this);
    }

    /// <summary>
    /// 设置消息值（通过编译的表达式树委托）。
    /// </summary>
    private void SetValue(BubblingChange value)
    {
        // 使用编译的委托设置只读属性的后备字段
        ValueSetter?.Invoke(this, value);
    }
}

/// <summary>
/// 高性能对象池实现，使用 ThreadLocal + 共享池模式。
/// 参考 System.Buffers.ArrayPool 的设计，减少跨线程竞争。
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
internal sealed class ObjectPool<T> where T : class
{
    private readonly Func<T> _factory;
    private readonly int _maxSizePerThread;
    private readonly int _maxSharedSize;

    // 每线程本地缓存，无锁访问
    [ThreadStatic]
    private static T[]? t_localPool;
    [ThreadStatic]
    private static int t_localCount;

    // 共享池，用于跨线程平衡
    private readonly System.Collections.Concurrent.ConcurrentQueue<T> _sharedPool = new();
    private int _sharedCount; // 原子计数器

    public ObjectPool(Func<T> factory, int maxSizePerThread = 16, int maxSharedSize = 128)
    {
        _factory = factory;
        _maxSizePerThread = maxSizePerThread;
        _maxSharedSize = maxSharedSize;
    }

    public T Get()
    {
        // 快速路径：从线程本地池获取
        var localPool = t_localPool;
        var localCount = t_localCount;

        if (localPool is not null && localCount > 0)
        {
            var item = localPool[--localCount];
            localPool[localCount] = null!;
            t_localCount = localCount;
            return item;
        }

        // 慢速路径：从共享池获取
        if (_sharedPool.TryDequeue(out var sharedItem))
        {
            Interlocked.Decrement(ref _sharedCount);
            return sharedItem;
        }

        // 创建新对象
        return _factory();
    }

    public void Return(T item)
    {
        // 快速路径：归还到线程本地池
        var localPool = t_localPool;
        if (localPool is null)
        {
            localPool = new T[_maxSizePerThread];
            t_localPool = localPool;
        }

        var localCount = t_localCount;
        if (localCount < _maxSizePerThread)
        {
            localPool[localCount] = item;
            t_localCount = localCount + 1;
            return;
        }

        // 本地池已满，尝试归还到共享池
        if (Volatile.Read(ref _sharedCount) < _maxSharedSize)
        {
            _sharedPool.Enqueue(item);
            Interlocked.Increment(ref _sharedCount);
        }
        // 否则丢弃对象，让 GC 回收
    }
}
