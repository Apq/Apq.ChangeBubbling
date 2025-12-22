# Apq.ChangeBubbling

变更冒泡事件库，提供树形数据结构的变更事件自动冒泡、Rx 响应式流、弱引用消息和可插拔调度环境。

## 项目结构

```text
Apq.ChangeBubbling/
├── Apq.ChangeBubbling/                         # 主库项目
├── Samples/
│   └── Apq.ChangeBubbling.Samples/             # 示例项目
├── tests/
│   ├── Apq.ChangeBubbling.Tests.Net6/          # .NET 6 测试项目
│   ├── Apq.ChangeBubbling.Tests.Net8/          # .NET 8 测试项目
│   ├── Apq.ChangeBubbling.Tests.Net9/          # .NET 9 测试项目
│   └── Apq.ChangeBubbling.Tests.Shared/        # 共享测试代码
└── benchmarks/
    ├── Apq.ChangeBubbling.Benchmarks.Net6/     # .NET 6 性能测试
    ├── Apq.ChangeBubbling.Benchmarks.Net8/     # .NET 8 性能测试
    ├── Apq.ChangeBubbling.Benchmarks.Net9/     # .NET 9 性能测试
    └── Apq.ChangeBubbling.Benchmarks.Shared/   # 共享性能测试代码
```

## 特性

- 变更事件冒泡
- Rx 响应式流 + 弱引用消息
- 可插拔调度环境
- 事件过滤与节流
- TPL Dataflow 背压管线
- 批量操作与事件合并
- 快照导出与导入
- 线程安全集合节点

## 支持的框架

.NET 6.0 / 7.0 / 8.0 / 9.0

## 快速开始

```csharp
using Apq.ChangeBubbling.Nodes;

// 创建节点树
var root = new ListBubblingNode<string>("Root");
var child = new ListBubblingNode<int>("Child");
root.AttachChild(child);

// 订阅变更事件
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"变更: {change.PropertyName}, 路径: {string.Join(".", change.PathSegments)}");
};

// 子节点的变更会自动冒泡到父节点
child.Add(42);  // 输出: 变更: 0, 路径: Child.0
```

## 构建与测试

```bash
dotnet build
dotnet test
```

## 性能测试

使用 BenchmarkDotNet 进行性能测试：

```bash
# 运行 .NET 9 性能测试
cd benchmarks/Apq.ChangeBubbling.Benchmarks.Net9
dotnet run -c Release

# 运行特定基准测试
dotnet run -c Release -- --filter *NodeBenchmarks*
```

性能测试包含：
- **BubblingChangeBenchmarks** - BubblingChange 结构体创建性能
- **NodeBenchmarks** - ListBubblingNode 和 DictionaryBubblingNode 操作性能
- **MessengerBenchmarks** - 消息发布和对象池性能

## 许可证

MIT License

## 作者

- 邮箱：amwpfiqvy@163.com
