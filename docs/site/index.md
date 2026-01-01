---
layout: home

hero:
  name: Apq.ChangeBubbling
  text: 变更冒泡事件库
  tagline: 支持 Rx 响应式流、弱引用消息和可插拔调度环境
  image:
    src: /logo.svg
    alt: Apq.ChangeBubbling
  actions:
    - theme: brand
      text: 快速开始
      link: /guide/
    - theme: alt
      text: 在 Gitee 上查看
      link: https://gitee.com/apq/Apq.ChangeBubbling

features:
  - icon: 🌳
    title: 变更事件冒泡
    details: 子节点的变更事件自动向上冒泡到父节点，携带完整路径信息，轻松追踪数据变化来源
  - icon: 📡
    title: Rx 响应式流
    details: 基于 System.Reactive 的响应式编程支持，提供节流、缓冲、过滤等丰富的流操作
  - icon: 💬
    title: 弱引用消息
    details: 集成 CommunityToolkit.Mvvm 弱引用消息，避免内存泄漏，支持跨组件通信
  - icon: ⚡
    title: 可插拔调度
    details: 支持线程池、UI 线程、专用线程、Nito.AsyncEx 等多种调度环境
  - icon: 🎯
    title: 事件过滤
    details: 内置属性过滤、路径过滤、频率节流等多种过滤器，精确控制事件传播
  - icon: 📸
    title: 快照服务
    details: 支持节点树的快照导出与导入，方便状态持久化和恢复
---

<div class="vp-doc" style="padding: 2rem;">

## 快速安装

::: code-group

```bash [.NET CLI]
dotnet add package Apq.ChangeBubbling
```

```xml [PackageReference]
<PackageReference Include="Apq.ChangeBubbling" Version="1.0.*" />
```

:::

## 简单示例

```csharp
using Apq.ChangeBubbling.Nodes;

// 创建节点树
var root = new ListBubblingNode<string>("Root");
var child = new ListBubblingNode<int>("Child");

// 建立父子关系
root.AttachChild(child);

// 订阅变更事件
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"变更: {change.PropertyName}, 路径: {string.Join(".", change.PathSegments)}");
};

// 子节点的变更会自动冒泡到父节点
child.Add(42);  // 输出: 变更: 0, 路径: Child.0
```

## 核心特性

| 特性 | 说明 |
|------|------|
| 变更冒泡 | 子节点变更自动冒泡到父节点，携带完整路径 |
| Rx 响应式 | 支持 Observable 流、节流、缓冲、过滤 |
| 弱引用消息 | 避免内存泄漏，自动清理失效订阅 |
| 批量操作 | BeginBatch/EndBatch 收集并批量触发事件 |
| 事件合并 | BeginCoalesce/EndCoalesce 合并同属性多次变更 |
| 背压管线 | 基于 TPL Dataflow 的背压处理管线 |
| 快照服务 | 节点树快照导出与导入 |
| 线程安全 | 提供 ConcurrentBag/ConcurrentDictionary 节点 |

## 节点类型

| 类型 | 描述 |
|------|------|
| `ListBubblingNode<T>` | 基于列表的冒泡节点 |
| `DictionaryBubblingNode<TKey, TValue>` | 基于字典的冒泡节点 |
| `ConcurrentBagBubblingNode<T>` | 线程安全的列表冒泡节点 |
| `ConcurrentDictionaryBubblingNode<TKey, TValue>` | 线程安全的字典冒泡节点 |

## 支持的框架

| 框架 | 版本 |
|------|------|
| .NET | 8.0, 10.0 (LTS) |

</div>
