# Apq.ChangeBubbling 性能基准测试

[![Gitee](https://img.shields.io/badge/Gitee-仓库-red)](https://gitee.com/apq/Apq.ChangeBubbling)

**仓库地址**：https://gitee.com/apq/Apq.ChangeBubbling

本目录包含 Apq.ChangeBubbling 变更冒泡库的性能基准测试，使用 [BenchmarkDotNet](https://benchmarkdotnet.org/) 框架。

## 项目结构

```
benchmarks/
└── Apq.ChangeBubbling.Benchmarks/        # 多目标框架基准测试项目
    ├── Apq.ChangeBubbling.Benchmarks.csproj  # 支持 net6.0;net8.0;net9.0
    ├── BenchmarkConfig.cs                # 自定义基准测试配置
    ├── BubblingChangeBenchmarks.cs       # BubblingChange 结构体性能测试
    ├── NodeBenchmarks.cs                 # 节点操作性能测试
    ├── MessengerBenchmarks.cs            # 消息系统性能测试
    └── Program.cs                        # 入口程序
```

## 基准测试类说明

### 1. BubblingChangeBenchmarks - 变更结构体性能测试

测试 `BubblingChange` 结构体的创建性能：

| 测试方法 | 说明 |
|----------|------|
| CreateBubblingChange | 创建基本变更对象 |
| CreateBubblingChangeWithPath | 创建带路径的变更对象 |

### 2. NodeBenchmarks - 节点操作性能测试

测试不同节点类型的增删操作性能：

| 测试方法 | 说明 |
|----------|------|
| ListNode_Add | List 节点添加元素 |
| ListNode_AddAndRemove | List 节点添加并移除元素 |
| DictNode_Put | Dictionary 节点添加键值对 |
| DictNode_PutAndRemove | Dictionary 节点添加并移除键值对 |

### 3. MessengerBenchmarks - 消息系统性能测试

测试消息发布和对象池性能：

| 测试方法 | 说明 |
|----------|------|
| Publish_SingleChange | 发布单个变更消息 |
| RentAndReturn_Message | 从对象池租借并归还消息 |

## 运行基准测试

> **说明**：以下所有命令均在**项目根目录**执行，测试结果保存在测试项目目录下。

### 测试配置说明

本项目使用自定义 `BenchmarkConfig` 配置，自动对比 .NET 6/8/9 三个版本的性能。

- **迭代次数**：5 次预热 + 10 次实际测试
- **预计耗时**：全部测试约 **6-8 分钟**完成
- **测试覆盖**：8 个测试方法 × 3 个运行时 = 24 个测试点
- **导出格式**：自动生成 Markdown、HTML、CSV 三种格式报告

### 基本运行

```bash
# 运行所有基准测试（Release 模式必须）
# 使用 .NET 9 作为宿主运行，自动测试 .NET 6/8/9 三个版本
# 结果自动保存到带时间戳的子目录
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *
```

### 运行特定测试

```bash
# 运行特定测试类
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *BubblingChangeBenchmarks*
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *NodeBenchmarks*
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *MessengerBenchmarks*

# 运行特定测试方法
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *ListNode_Add*

# 组合多个过滤器
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *Node* --filter *Messenger*
```

> **注意**：`--` 是必须的，它将后面的参数传递给 BenchmarkDotNet 而不是 dotnet 命令。

### 其他选项

```bash
# 列出所有可用测试（不实际运行）
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --list flat
```

> **说明**：导出格式（Markdown、HTML、CSV）已在 `BenchmarkConfig` 中配置，无需手动指定。

## 测试结果

运行完成后，结果保存在带时间戳的子目录中，便于追踪历史性能变化：

```
benchmarks/Apq.ChangeBubbling.Benchmarks/
└── BenchmarkDotNet.Artifacts/
    ├── 2024-12-24_143052/              # 按时间戳保留
    │   └── results/
    │       ├── *-report.csv            # CSV 格式数据
    │       ├── *-report.html           # HTML 可视化报告
    │       └── *-report-github.md      # GitHub Markdown 格式
    ├── 2024-12-25_091530/              # 另一次测试
    │   └── results/
    └── ...
```

### 多版本对比结果示例

使用 `--runtimes` 参数后，结果会包含各版本的对比：

| Method | Runtime | Mean | Allocated |
|--------|---------|------|-----------|
| Test   | .NET 6  | 100ns | 64 B |
| Test   | .NET 8  | 80ns | 64 B |
| Test   | .NET 9  | 75ns | 64 B |

### 结果解读

| 列名 | 说明 |
|------|------|
| Mean | 平均执行时间 |
| Error | 误差范围 |
| StdDev | 标准差 |
| Median | 中位数 |
| Rank | 性能排名 |
| Gen0/Gen1/Gen2 | GC 代数统计 |
| Allocated | 内存分配量 |

## 注意事项

1. **必须使用 Release 模式** - Debug 模式结果不准确
2. **必须指定框架** - 多目标项目需要 `-f net9.0` 等参数
3. **关闭其他程序** - 减少系统干扰
4. **自定义配置** - 使用 BenchmarkConfig（5 次预热 + 10 次迭代），全部测试约 6-8 分钟完成
