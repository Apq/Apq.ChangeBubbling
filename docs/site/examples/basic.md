# 基础示例

本节展示 Apq.ChangeBubbling 的基础用法。

## 创建节点树

```csharp
using Apq.ChangeBubbling.Nodes;

// 创建根节点
var root = new ListBubblingNode<object>("Root");

// 创建子节点
var users = new ListBubblingNode<User>("Users");
var settings = new DictionaryBubblingNode<string, object>("Settings");

// 附加子节点
root.AttachChild(users);
root.AttachChild(settings);
```

## 订阅变更事件

```csharp
// 订阅根节点，接收所有子节点的变更
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"[{change.Kind}] {change.NodeName}");
    Console.WriteLine($"  Path: {change.Path}");
    Console.WriteLine($"  Property: {change.PropertyName}");
    Console.WriteLine($"  OldValue: {change.OldValue}");
    Console.WriteLine($"  NewValue: {change.NewValue}");
    Console.WriteLine();
};
```

## 操作列表节点

```csharp
// 添加元素
users.Add(new User { Name = "Alice", Email = "alice@example.com" });
// 输出: [CollectionAdd] Users

// 插入元素
users.Insert(0, new User { Name = "Bob", Email = "bob@example.com" });
// 输出: [CollectionAdd] Users

// 移除元素
users.RemoveAt(0);
// 输出: [CollectionRemove] Users

// 访问元素
Console.WriteLine($"User count: {users.Count}");
foreach (var user in users.Items)
{
    Console.WriteLine($"  - {user.Name}");
}
```

## 操作字典节点

```csharp
// 添加/更新
settings.Put("Theme", "Dark");
// 输出: [CollectionAdd] Settings

settings.Put("FontSize", 14);
// 输出: [CollectionAdd] Settings

settings.Put("Theme", "Light"); // 更新
// 输出: [CollectionReplace] Settings

// 获取值
if (settings.TryGet("Theme", out var theme))
{
    Console.WriteLine($"Theme: {theme}");
}

// 移除
settings.Remove("FontSize");
// 输出: [CollectionRemove] Settings
```

## 完整示例

```csharp
using Apq.ChangeBubbling.Nodes;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class Program
{
    public static void Main()
    {
        // 创建节点树
        var root = new ListBubblingNode<object>("Root");
        var users = new ListBubblingNode<User>("Users");
        var settings = new DictionaryBubblingNode<string, object>("Settings");

        root.AttachChild(users);
        root.AttachChild(settings);

        // 订阅变更
        root.NodeChanged += (sender, change) =>
        {
            Console.WriteLine($"[{change.Kind}] {change.Path}");
        };

        // 操作数据
        users.Add(new User { Name = "Alice" });
        users.Add(new User { Name = "Bob" });
        settings.Put("Theme", "Dark");
        settings.Put("Language", "zh-CN");

        Console.WriteLine($"\nUsers: {users.Count}");
        Console.WriteLine($"Settings: {settings.Count}");
    }
}
```

输出：
```
[CollectionAdd] Root/Users/[0]
[CollectionAdd] Root/Users/[1]
[CollectionAdd] Root/Settings/Theme
[CollectionAdd] Root/Settings/Language

Users: 2
Settings: 2
```
