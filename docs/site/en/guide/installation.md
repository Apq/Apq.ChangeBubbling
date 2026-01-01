# Installation

## System Requirements

- .NET 8.0 or .NET 10.0

## Using .NET CLI

```bash
dotnet add package Apq.ChangeBubbling
```

## Using PackageReference

Add to your `.csproj` file:

```xml
<PackageReference Include="Apq.ChangeBubbling" Version="1.0.*" />
```

## Using NuGet Package Manager

In Visual Studio:

1. Right-click on project
2. Select "Manage NuGet Packages"
3. Search for "Apq.ChangeBubbling"
4. Click Install

## Dependencies

Apq.ChangeBubbling depends on:

- System.Reactive (for Rx streams)
- System.Threading.Tasks.Dataflow (for backpressure pipeline)

## Verify Installation

```csharp
using Apq.ChangeBubbling.Nodes;

var node = new ListBubblingNode<string>("Test");
node.Add("Hello");
Console.WriteLine($"Count: {node.Count}"); // Output: Count: 1
```

## Next Steps

- [Quick Start](/en/guide/quick-start) - Get started quickly
- [Node Types](/en/guide/node-types) - Learn about node types
