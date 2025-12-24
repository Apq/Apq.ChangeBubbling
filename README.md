# Apq.ChangeBubbling

[![Gitee](https://img.shields.io/badge/Gitee-仓库-red)](https://gitee.com/apq/Apq.ChangeBubbling)

变更冒泡事件库，提供树形数据结构的变更事件自动冒泡、Rx 响应式流、弱引用消息和可插拔调度环境。

**仓库地址**：https://gitee.com/apq/Apq.ChangeBubbling

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
    └── Apq.ChangeBubbling.Benchmarks/          # 性能测试项目（多目标框架）
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

// 建立父子关系
root.AttachChild(child);

// 订阅变更事件
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"变更: {change.PropertyName}, 类型: {change.Kind}, 路径: {string.Join(".", change.PathSegments)}");
};

// 子节点的变更会自动冒泡到父节点
child.Add(42);
child.Add(100);
```

## 构建与测试

```bash
dotnet build
dotnet test
```

## 单元测试覆盖

共 **246** 个单元测试，覆盖所有核心功能模块。

### 测试通过情况

| 框架 | 通过 | 失败 | 跳过 | 状态 |
|------|------|------|------|------|
| .NET 6.0 | 246 | 0 | 0 | ✅ 全部通过 |
| .NET 8.0 | 246 | 0 | 0 | ✅ 全部通过 |
| .NET 9.0 | 246 | 0 | 0 | ✅ 全部通过 |

### 测试类明细

| 测试类 | 测试数 | 覆盖模块 |
|--------|--------|----------|
| BubblingChangeTests | 7 | BubblingChange 结构体基础功能 |
| ChangeNodeBaseTests | 12 | 节点基类、父子关系、事件冒泡、批量/合并模式 |
| ListBubblingNodeTests | 11 | 列表节点 CRUD 操作、事件触发 |
| DictionaryBubblingNodeTests | 12 | 字典节点 CRUD 操作、事件触发 |
| ConcurrentBagBubblingNodeTests | 12 | 线程安全列表节点、并发操作 |
| ConcurrentDictionaryBubblingNodeTests | 12 | 线程安全字典节点、并发操作 |
| ChangeMessengerTests | 16 | 消息中心、Rx 流、调度环境 |
| BubblingChangeMessageTests | 11 | 消息池化、对象复用 |
| EventFilterTests | 36 | 属性/路径/频率过滤器、组合过滤器 |
| DataflowTests | 10 | TPL Dataflow 管线、Rx 桥接 |
| TreeSnapshotServiceTests | 16 | 树形快照导出/导入 |
| MultiValueSnapshotServiceTests | 19 | 多值容器快照服务 |
| SnapshotSerializerTests | 18 | JSON 序列化/反序列化 |
| ChangeBubblingMetricsTests | 21 | 性能指标收集与统计 |
| ObservableCollectionAdapterTests | 21 | 集合适配器、代理事件 |
| NitoAsyncContextEnvironmentTests | 12 | Nito 异步上下文环境 |

### 测试覆盖的核心功能

- **变更冒泡机制** - 子节点变更自动冒泡到父节点
- **批量操作** - BeginBatch/EndBatch 收集并批量触发事件
- **事件合并** - BeginCoalesce/EndCoalesce 合并同属性多次变更
- **线程安全** - ConcurrentBag/ConcurrentDictionary 节点并发测试
- **事件过滤** - 属性、路径、频率、组合过滤器
- **消息系统** - Rx 响应式流、弱引用消息、对象池
- **快照服务** - 树形结构和多值容器的导出/导入
- **性能指标** - 事件计数、处理时间、订阅统计

## 性能测试

使用 BenchmarkDotNet 进行性能测试：

```bash
# 运行性能测试
cd benchmarks/Apq.ChangeBubbling.Benchmarks
dotnet run -c Release

# 运行特定基准测试
dotnet run -c Release -- --filter *NodeBenchmarks*
```

性能测试包含：
- **BubblingChangeBenchmarks** - BubblingChange 结构体创建性能
- **NodeBenchmarks** - ListBubblingNode 和 DictionaryBubblingNode 操作性能
- **MessengerBenchmarks** - 消息发布和对象池性能

### 性能测试结果

测试环境：Windows 11, .NET SDK 9.0.308, BenchmarkDotNet v0.14.0

#### BubblingChange 创建性能

| 方法 | .NET 6 | .NET 8 | .NET 9 | 内存分配 |
|------|--------|--------|--------|----------|
| CreateBubblingChange | 5.24 ns | 3.07 ns | **3.04 ns** | 24 B |
| CreateBubblingChangeWithPath | 9.32 ns | 4.81 ns | **5.05 ns** | 72 B |

#### Messenger 消息性能

| 方法 | .NET 6 | .NET 8 | .NET 9 | 内存分配 |
|------|--------|--------|--------|----------|
| Publish_SingleChange | 950.4 ns | 495.9 ns | **323.7 ns** | 456 B |
| RentAndReturn_Message | 20.0 ns | 23.0 ns | **11.5 ns** | 0 B |

> 消息池租借/归还实现了零 GC 分配

#### 性能总结

| 运行时 | 相对性能 |
|--------|---------|
| .NET 6 | 基准 (1.0x) |
| .NET 8 | 快 1.7-2.0x |
| .NET 9 | 快 1.7-2.9x |

**推荐**：在 .NET 9 环境下运行可获得最佳性能。

## 许可证

MIT License

## 作者

- 邮箱：amwpfiqvy@163.com
