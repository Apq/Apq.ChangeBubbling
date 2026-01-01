# 节点类型 API

## IChangeNode

节点接口，所有节点类型都实现此接口。

```csharp
public interface IChangeNode
{
    string NodeName { get; }
    IChangeNode? Parent { get; }
    IReadOnlyList<IChangeNode> Children { get; }
    event EventHandler<BubblingChange>? NodeChanged;
    void AttachChild(IChangeNode child);
    void DetachChild(IChangeNode child);
}
```

## ListBubblingNode&lt;T&gt;

列表节点，用于管理有序集合。

### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Items` | `IReadOnlyList<T>` | 只读元素列表 |
| `Count` | `int` | 元素数量 |

### 方法

| 方法 | 说明 |
|------|------|
| `Add(T item)` | 添加元素 |
| `Insert(int index, T item)` | 在指定位置插入元素 |
| `RemoveAt(int index)` | 移除指定位置的元素 |
| `Remove(T item)` | 移除指定元素 |
| `PopulateSilently(IEnumerable<T> items)` | 静默填充（不触发事件） |

## DictionaryBubblingNode&lt;TKey, TValue&gt;

字典节点，用于管理键值对集合。

### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Keys` | `IReadOnlyCollection<TKey>` | 只读键集合 |
| `Items` | `IReadOnlyDictionary<TKey, TValue>` | 只读字典 |
| `Count` | `int` | 元素数量 |

### 方法

| 方法 | 说明 |
|------|------|
| `Put(TKey key, TValue value)` | 添加或更新键值对 |
| `TryGet(TKey key, out TValue value)` | 尝试获取值 |
| `Remove(TKey key)` | 移除键值对 |
| `PopulateSilently(IEnumerable<KeyValuePair<TKey, TValue>> items)` | 静默填充 |

## ConcurrentBagBubblingNode&lt;T&gt;

线程安全的列表节点。

### 方法

| 方法 | 说明 |
|------|------|
| `Add(T item)` | 线程安全添加元素 |
| `Remove(T item)` | 线程安全移除元素 |
| `Clear()` | 清空所有元素 |

## ConcurrentDictionaryBubblingNode&lt;TKey, TValue&gt;

线程安全的字典节点。

### 方法

| 方法 | 说明 |
|------|------|
| `Put(TKey key, TValue value)` | 线程安全添加或更新 |
| `TryGet(TKey key, out TValue value)` | 线程安全获取 |
| `Remove(TKey key)` | 线程安全移除 |
