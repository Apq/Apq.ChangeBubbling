using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Apq.ChangeBubbling.Abstractions;
using Apq.ChangeBubbling.Infrastructure.Performance;

namespace Apq.ChangeBubbling.Collections;

/// <summary>
/// 基于 Castle DynamicProxy 的通用集合适配器：为 IList/ICollection/IDictionary 等集合生成代理，
/// 截获修改方法并触发 BubblingChange。
/// 注意：该适配器聚焦"通知"，不强行统一所有集合 API，仅拦截常见变更入口。
/// </summary>
public sealed class ObservableCollectionAdapter : IMultiValueStore
{
    private static readonly ProxyGenerator SharedGenerator = new();

    // 缓存类型到代理接口的映射，避免重复反射查找
    private static readonly ConcurrentDictionary<Type, Type?> _interfaceCache = new();

    // 缓存代理生成选项，避免重复创建
    private static readonly ProxyGenerationOptions _proxyOptions = new();

    private readonly object _target;
    private readonly IEnumerable _enumerable;
    private readonly string _name;
    private readonly CollectionChangeInterceptor _interceptor;

    public ObservableCollectionAdapter(object targetCollection, string name)
    {
        ArgumentNullException.ThrowIfNull(targetCollection);
        ArgumentNullException.ThrowIfNull(name);
        _target = targetCollection;
        _enumerable = (IEnumerable)targetCollection;
        _name = name;
        _interceptor = new CollectionChangeInterceptor(name, Raise);
        Proxied = CreateProxy(_target);
    }

    /// <summary>
    /// 代理对象，供上层替代原集合引用进行使用。
    /// </summary>
    public object Proxied { get; }

    public string Name => _name;

    public int Count
    {
        get
        {
            if (_target is ICollection col) return col.Count;
            var c = 0; foreach (var _ in _enumerable) c++;
            return c;
        }
    }

    public IEnumerable<object?> Items
    {
        get
        {
            foreach (var i in _enumerable) yield return i;
        }
    }

    public event EventHandler<BubblingChange>? NodeChanged;

    public IEnumerator GetEnumerator() => _enumerable.GetEnumerator();

    private object CreateProxy(object target)
    {
        var type = target.GetType();

        // 使用缓存的接口类型
        var proxyInterface = _interfaceCache.GetOrAdd(type, static t => FindBestInterface(t));

        if (proxyInterface is not null)
        {
            return SharedGenerator.CreateInterfaceProxyWithTarget(proxyInterface, target, _proxyOptions, _interceptor);
        }

        if (type.IsInterface)
        {
            return SharedGenerator.CreateInterfaceProxyWithTarget(type, target, _proxyOptions, _interceptor);
        }

        // Fallback: return original (no interception for non-virtual members)
        return target;
    }

    /// <summary>
    /// 查找最佳代理接口（静态方法，用于缓存）。
    /// </summary>
    private static Type? FindBestInterface(Type type)
    {
        // Prefer generic interfaces to satisfy typed casts in tests
        var genericDict = type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
        if (genericDict is not null)
            return genericDict;

        var genericList = type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IList<>));
        if (genericList is not null)
            return genericList;

        var genericCollection = type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>));
        if (genericCollection is not null)
            return genericCollection;

        // Then try non-generic base interfaces
        if (typeof(IDictionary).IsAssignableFrom(type))
            return typeof(IDictionary);

        if (typeof(IList).IsAssignableFrom(type))
            return typeof(IList);

        if (typeof(ICollection).IsAssignableFrom(type))
            return typeof(ICollection);

        return null;
    }

    private void Raise(BubblingChange change) => NodeChanged?.Invoke(this, change);

    private sealed class CollectionChangeInterceptor : IInterceptor
    {
        private readonly string _property;
        private readonly Action<BubblingChange> _raise;

        public CollectionChangeInterceptor(string property, Action<BubblingChange> raise)
        {
            _property = property;
            _raise = raise;
        }

        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            var isMutator = IsMutation(methodName);

            object? oldValue = null;
            object? key = null;
            int? index = null;
            bool existedBefore = false;

            // Pre-detect existence for dictionary set operations to distinguish Add vs Replace
            if (methodName is "set_Item" && invocation.Arguments.Length >= 2 && invocation.Arguments[0] is not null)
            {
                if (invocation.InvocationTarget is IDictionary dict0)
                {
                    existedBefore = dict0.Contains(invocation.Arguments[0]!);
                }
            }

            if (isMutator)
            {
                CaptureBefore(invocation, methodName, ref oldValue, ref key, ref index);
            }

            invocation.Proceed();

            if (isMutator)
            {
                var (kind, newValue) = MapKindAndNewValue(methodName, invocation.Arguments);
                if (methodName is "set_Item")
                {
                    // If key did not exist before, this is an Add rather than Replace
                    if (!existedBefore)
                    {
                        kind = NodeChangeKind.CollectionAdd;
                    }
                }
                var change = new BubblingChange
                {
                    PropertyName = _property,
                    Kind = kind,
                    PathSegments = PathSegmentCache.GetSingle(_property),
                    OldValue = oldValue,
                    NewValue = newValue,
                    Index = index,
                    Key = key
                };
                _raise(change);
            }
        }

        private static bool IsMutation(string methodName) => methodName switch
        {
            "Add" or "Remove" or "Insert" or "Clear" or "RemoveAt" or "RemoveAll" => true,
            "AddRange" or "InsertRange" or "RemoveRange" => true,
            "TryAdd" or "AddOrUpdate" or "Put" or "SetItem" or "set_Item" => true,
            _ => false
        };

        private static void CaptureBefore(IInvocation invocation, string methodName, ref object? oldValue, ref object? key, ref int? index)
        {
            var target = invocation.InvocationTarget;
            if (methodName is "set_Item" && invocation.Arguments.Length >= 2)
            {
                if (invocation.Arguments[0] is int i && target is IList list && i >= 0 && i < list.Count)
                {
                    index = i;
                    oldValue = list[i];
                }
                else if (target is IDictionary dict && invocation.Arguments[0] is not null)
                {
                    key = invocation.Arguments[0]!;
                    if (dict.Contains(key)) oldValue = dict[key];
                }
            }
            else if (methodName is nameof(IList.Insert) && invocation.Arguments.Length >= 2 && target is IList listIns)
            {
                if (invocation.Arguments[0] is int i)
                {
                    index = Math.Clamp(i, 0, listIns.Count);
                }
            }
            else if (methodName is nameof(IList.Add) && target is IList listAdd)
            {
                // Add at end; index before add equals current Count
                index = listAdd.Count;
            }
            else if (methodName is nameof(IDictionary.Add) && invocation.Arguments.Length >= 2 && target is IDictionary)
            {
                key = invocation.Arguments[0]!;
            }
            else if (methodName is nameof(IList.Remove) && invocation.Arguments.Length >= 1 && target is IList list2)
            {
                oldValue = invocation.Arguments[0];
            }
            else if (methodName is "RemoveAt" && invocation.Arguments.Length >= 1 && target is IList list3)
            {
                if (invocation.Arguments[0] is not int i) return;
                if (i >= 0 && i < list3.Count)
                {
                    index = i;
                    oldValue = list3[i];
                }
            }
            else if (methodName is nameof(IDictionary.Remove) && invocation.Arguments.Length >= 1 && target is IDictionary dict2 && invocation.Arguments[0] is not null)
            {
                key = invocation.Arguments[0]!;
                if (dict2.Contains(key)) oldValue = dict2[key];
            }
        }

        private static (NodeChangeKind kind, object? newValue) MapKindAndNewValue(string methodName, object?[] args)
        {
            if (methodName is nameof(IList.Add) or nameof(IList.Insert) or "AddRange" or "InsertRange" or "TryAdd" or "Put")
            {
                return (NodeChangeKind.CollectionAdd, args.Length > 0 ? args[^1] : null);
            }
            if (methodName is "AddOrUpdate")
            {
                // 区分添加或更新：根据参数个数与语义，若包含旧值替换则视为 Replace
                return (NodeChangeKind.CollectionReplace, args.Length > 1 ? args[^1] : null);
            }
            if (methodName is nameof(IDictionary.Add))
            {
                return (NodeChangeKind.CollectionAdd, args.Length > 1 ? args[1] : null);
            }
            if (methodName is "set_Item" or "SetItem")
            {
                return (NodeChangeKind.CollectionReplace, args.Length > 1 ? args[1] : null);
            }
            if (methodName is nameof(IList.Remove) or nameof(IDictionary.Remove) or "RemoveAt" or "RemoveRange" or "RemoveAll")
            {
                return (NodeChangeKind.CollectionRemove, null);
            }
            if (methodName is "Move" or "MoveItem")
            {
                return (NodeChangeKind.CollectionMove, null);
            }
            if (methodName is nameof(IList.Clear))
            {
                return (NodeChangeKind.CollectionReset, null);
            }
            return (NodeChangeKind.PropertyUpdate, null);
        }
    }
}


