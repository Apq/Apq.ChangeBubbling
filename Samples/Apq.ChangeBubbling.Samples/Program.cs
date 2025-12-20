using Apq.ChangeBubbling.Core;
using Apq.ChangeBubbling.Nodes;
using Apq.ChangeBubbling.Messaging;
using Apq.ChangeBubbling.Abstractions;

Console.WriteLine("=== Apq.ChangeBubbling 示例 ===\n");

// 示例 1: 基本用法 - 创建节点树
Console.WriteLine("1. 创建节点树并订阅变更事件:");

var root = new ListBubblingNode<string>("Root");
var child = new ListBubblingNode<int>("Child");

// 建立父子关系
root.AttachChild(child);

// 订阅变更事件
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"   变更: {change.PropertyName}, 类型: {change.Kind}, 路径: {string.Join(".", change.PathSegments)}");
};

// 子节点的变更会自动冒泡到父节点
Console.WriteLine("   添加元素到子节点:");
child.Add(42);
child.Add(100);

// 示例 2: 使用 Rx 响应式流
Console.WriteLine("\n2. 使用 Rx 响应式流:");

using var subscription = ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine($"   Rx 收到变更: {change.PropertyName}, 类型: {change.Kind}"));

// 发布变更到消息中心
var bubblingChange = new BubblingChange
{
    PropertyName = "TestProperty",
    Kind = NodeChangeKind.PropertyUpdate,
    OldValue = null,
    NewValue = "NewValue",
    PathSegments = new[] { "Root", "TestProperty" }
};
ChangeMessenger.Publish(bubblingChange);

// 示例 3: 批量操作
Console.WriteLine("\n3. 批量操作:");

child.BeginBatch();
child.Add(200);
child.Add(300);
child.Add(400);
child.EndBatch(); // 批量结束时触发一次合并事件

Console.WriteLine("\n=== 示例完成 ===");
