# 安装

## 系统要求

- .NET 8.0 或 .NET 10.0

## 使用 .NET CLI

```bash
dotnet add package Apq.ChangeBubbling
```

## 使用 PackageReference

在项目文件 (`.csproj`) 中添加：

```xml
<PackageReference Include="Apq.ChangeBubbling" Version="1.0.*" />
```

## 使用 NuGet 包管理器

在 Visual Studio 中：

1. 右键点击项目 → 管理 NuGet 包
2. 搜索 `Apq.ChangeBubbling`
3. 点击安装

## 依赖项

Apq.ChangeBubbling 依赖以下包（会自动安装）：

| 包名 | 用途 |
|------|------|
| System.Reactive | Rx 响应式编程 |
| System.Threading.Tasks.Dataflow | TPL Dataflow 背压管线 |
| CommunityToolkit.Mvvm | 弱引用消息 |
| Castle.Core | 动态代理 |
| Nito.AsyncEx | 异步上下文 |
| PropertyChanged.Fody | 自动织入 INotifyPropertyChanged |

## 验证安装

创建一个简单的测试程序：

```csharp
using Apq.ChangeBubbling.Nodes;

var node = new ListBubblingNode<string>("Test");
node.NodeChanged += (s, e) => Console.WriteLine($"变更: {e.PropertyName}");
node.Add("Hello");

Console.WriteLine("安装成功！");
```

运行程序，如果看到输出则表示安装成功。

## 下一步

- [快速开始](/guide/quick-start) - 5 分钟上手教程
- [节点类型](/guide/node-types) - 了解所有节点类型
