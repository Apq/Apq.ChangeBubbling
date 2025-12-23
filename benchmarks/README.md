# Apq.ChangeBubbling 性能基准测试

本目录包含 Apq.ChangeBubbling 变更冒泡库的性能基准测试，使用 [BenchmarkDotNet](https://benchmarkdotnet.org/) 框架。

## 项目结构

```
benchmarks/
├── Apq.ChangeBubbling.Benchmarks.Shared/     # 共享的基准测试类
│   ├── BubblingChangeBenchmarks.cs           # BubblingChange 结构体性能测试
│   ├── NodeBenchmarks.cs                     # 节点操作性能测试
│   ├── MessengerBenchmarks.cs                # 消息系统性能测试
│   └── Program.cs                            # 入口程序
├── Apq.ChangeBubbling.Benchmarks.Net6/       # .NET 6 测试项目
├── Apq.ChangeBubbling.Benchmarks.Net8/       # .NET 8 测试项目
└── Apq.ChangeBubbling.Benchmarks.Net9/       # .NET 9 测试项目
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

### 基本运行

```bash
# 运行所有基准测试（Release 模式必须）
# 重要：必须使用 -- --filter * 指定测试，否则会进入交互模式等待输入
# 注意：--artifacts 参数确保结果保存在各项目目录下，避免冲突

# .NET 6
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net6 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net6/BenchmarkDotNet.Artifacts

# .NET 8
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net8 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net8/BenchmarkDotNet.Artifacts

# .NET 9
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
```

### 运行特定测试

```bash
# 运行特定测试类（以 .NET 9 为例，其他版本替换项目名即可）
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter *BubblingChangeBenchmarks* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter *NodeBenchmarks* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter *MessengerBenchmarks* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts

# 运行特定测试方法
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter *ListNode_Add* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter *Publish_SingleChange* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts

# 组合多个过滤器
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter *Node* --filter *Messenger* --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
```

> **注意**：`--` 是必须的，它将后面的参数传递给 BenchmarkDotNet 而不是 dotnet 命令。

### 常用参数

```bash
# 快速测试（减少迭代次数，用于验证功能是否正常）
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter *NodeBenchmarks* --job short --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts

# 运行所有测试的快速版本
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter * --job short --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts

# 列出所有可用测试（不实际运行）
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --list flat

# 导出为不同格式
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter * --exporters markdown --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter * --exporters html --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter * --exporters csv --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
```

### 高级选项

```bash
# 指定运行时
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net6 -f net6.0 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net6/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net8 -f net8.0 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net8/BenchmarkDotNet.Artifacts
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -f net9.0 -- --filter * --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts

# 内存诊断（默认已启用）
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter * --memory --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts

# 显示详细信息
dotnet run -c Release --project benchmarks/Apq.ChangeBubbling.Benchmarks.Net9 -- --filter * --info --artifacts benchmarks/Apq.ChangeBubbling.Benchmarks.Net9/BenchmarkDotNet.Artifacts
```

## 并行运行多个测试（CPU 亲和性隔离）

在多核机器上（如 64G 内存、32 核），可以同时运行多个性能测试，使用 CPU 亲和性隔离避免相互干扰。

### 8 核分配方案（运行 2 个版本，需 16 核）

```powershell
# Net6，绑定到 CPU 0-7
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net6" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0xFF }

# Net8，绑定到 CPU 8-15
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net8" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0xFF00 }
```

### 8 核分配方案（运行 3 个版本，需 24 核）

```powershell
# Net6，绑定到 CPU 0-7
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net6" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0xFF }

# Net8，绑定到 CPU 8-15
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net8" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0xFF00 }

# Net9，绑定到 CPU 16-23
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net9" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0xFF0000 }
```

### 16 核分配方案（运行 2 个版本，需 32 核）

```powershell
# Net6，绑定到 CPU 0-15
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net6" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0xFFFF }

# Net8，绑定到 CPU 16-31
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net8" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0xFFFF0000 }
```

### 10 核分配方案（运行 3 个版本，需 30 核）

```powershell
# Net6，绑定到 CPU 0-9
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net6" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0x3FF }

# Net8，绑定到 CPU 10-19
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net8" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0xFFC00 }

# Net9，绑定到 CPU 20-29
Start-Process -FilePath "dotnet" -ArgumentList "run -c Release -- --filter *" -WorkingDirectory "D:\ApqGitee\Apq.ChangeBubbling\benchmarks\Apq.ChangeBubbling.Benchmarks.Net9" -PassThru | ForEach-Object { $_.ProcessorAffinity = 0x3FF00000 }
```

### 亲和性掩码参考

| 亲和性掩码 | 核心范围 |
|-----------|---------|
| `0xFF` | CPU 0-7 |
| `0xFF00` | CPU 8-15 |
| `0xFF0000` | CPU 16-23 |
| `0xFF000000` | CPU 24-31 |
| `0xFFFF` | CPU 0-15 |
| `0xFFFF0000` | CPU 16-31 |
| `0x3FF` | CPU 0-9 |
| `0xFFC00` | CPU 10-19 |
| `0x3FF00000` | CPU 20-29 |

## 测试结果

运行完成后，结果保存在各项目目录下的 `BenchmarkDotNet.Artifacts/results/` 目录：

```
benchmarks/
├── Apq.ChangeBubbling.Benchmarks.Net6/
│   └── BenchmarkDotNet.Artifacts/results/    # .NET 6 测试结果
├── Apq.ChangeBubbling.Benchmarks.Net8/
│   └── BenchmarkDotNet.Artifacts/results/    # .NET 8 测试结果
└── Apq.ChangeBubbling.Benchmarks.Net9/
    └── BenchmarkDotNet.Artifacts/results/    # .NET 9 测试结果

# 每个 results 目录包含：
├── *-report.csv          # CSV 格式数据
├── *-report.html         # HTML 可视化报告
└── *-report-github.md    # GitHub Markdown 格式
```

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
2. **关闭其他程序** - 减少系统干扰
3. **多次运行** - BenchmarkDotNet 会自动预热和多次迭代
4. **结果对比** - 使用相同环境进行对比测试
