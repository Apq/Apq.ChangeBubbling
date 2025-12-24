# Apq.ChangeBubbling 性能基准测试

[![Gitee](https://img.shields.io/badge/Gitee-仓库-red)](https://gitee.com/apq/Apq.ChangeBubbling)

**仓库地址**：https://gitee.com/apq/Apq.ChangeBubbling

本目录包含 Apq.ChangeBubbling 变更冒泡库的性能基准测试，使用 [BenchmarkDotNet](https://benchmarkdotnet.org/) 框架。

## 项目结构

```
benchmarks/
└── Apq.ChangeBubbling.Benchmarks/        # 多目标框架基准测试项目
    ├── Apq.ChangeBubbling.Benchmarks.csproj  # 支持 net6.0;net8.0;net9.0
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

### 基本运行

```bash
# 运行所有基准测试（Release 模式必须）
# 重要：多目标框架项目必须使用 -f 指定框架

# 使用 .NET 9 运行
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts

# 使用 .NET 8 运行
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net8.0 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts

# 使用 .NET 6 运行
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net6.0 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
```

### 多版本对比测试

BenchmarkDotNet 支持在一次运行中对比多个 .NET 版本的性能：

```bash
# 同时测试 .NET 6、8、9 的性能对比
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter * --runtimes net6.0 net8.0 net9.0 --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
```

### 运行特定测试

```bash
# 运行特定测试类
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *BubblingChangeBenchmarks* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *NodeBenchmarks* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *MessengerBenchmarks* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts

# 运行特定测试方法
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *ListNode_Add* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *Publish_SingleChange* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts

# 组合多个过滤器
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter *Node* --filter *Messenger* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
```

> **注意**：`--` 是必须的，它将后面的参数传递给 BenchmarkDotNet 而不是 dotnet 命令。

### 常用参数

```bash
# 快速测试（减少迭代次数，用于验证功能是否正常）
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter * --job short --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts

# 列出所有可用测试（不实际运行）
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --list flat

# 导出为不同格式
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter * --exporters markdown --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter * --exporters html --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter * --exporters csv --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
```

### 高级选项

```bash
# 内存诊断（默认已启用）
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter * --memory --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts

# 显示详细信息
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks -f net9.0 -- --filter * --info --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks/BenchmarkDotNet.Artifacts
```

## 测试结果

运行完成后，结果默认保存在 `BenchmarkDotNet.Artifacts/results/` 目录：

```
benchmarks/Apq.ChangeBubbling.Benchmarks/
└── BenchmarkDotNet.Artifacts/
    └── results/
        ├── *-report.csv          # CSV 格式数据
        ├── *-report.html         # HTML 可视化报告
        └── *-report-github.md    # GitHub Markdown 格式
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
4. **多次运行** - BenchmarkDotNet 会自动预热和多次迭代
5. **结果对比** - 使用 `--runtimes` 参数可在一次运行中对比多个版本
