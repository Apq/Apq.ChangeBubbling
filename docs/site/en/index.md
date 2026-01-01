---
layout: home

hero:
  name: Apq.ChangeBubbling
  text: Change Bubbling Event Library
  tagline: Rx reactive streams, weak messaging, and pluggable scheduling
  image:
    src: /logo.svg
    alt: Apq.ChangeBubbling
  actions:
    - theme: brand
      text: Get Started
      link: /en/guide/
    - theme: alt
      text: View on Gitee
      link: https://gitee.com/apq/Apq.ChangeBubbling

features:
  - icon: 🌳
    title: Change Event Bubbling
    details: Child node changes automatically bubble up to parent nodes with full path information
  - icon: 📡
    title: Rx Reactive Streams
    details: System.Reactive based reactive programming with throttling, buffering, and filtering
  - icon: 💬
    title: Weak Reference Messaging
    details: CommunityToolkit.Mvvm integration for memory-safe cross-component communication
  - icon: ⚡
    title: Pluggable Scheduling
    details: Support for thread pool, UI thread, dedicated threads, and Nito.AsyncEx
  - icon: 🎯
    title: Event Filtering
    details: Built-in property, path, and frequency filters for precise event control
  - icon: 📸
    title: Snapshot Service
    details: Export and import node tree snapshots for state persistence
---

<div class="vp-doc" style="padding: 2rem;">

## Quick Install

::: code-group

```bash [.NET CLI]
dotnet add package Apq.ChangeBubbling
```

```xml [PackageReference]
<PackageReference Include="Apq.ChangeBubbling" Version="1.0.*" />
```

:::

## Simple Example

```csharp
using Apq.ChangeBubbling.Nodes;

// Create node tree
var root = new ListBubblingNode<string>("Root");
var child = new ListBubblingNode<int>("Child");

// Establish parent-child relationship
root.AttachChild(child);

// Subscribe to change events
root.NodeChanged += (sender, change) =>
{
    Console.WriteLine($"Change: {change.PropertyName}, Path: {string.Join(".", change.PathSegments)}");
};

// Child changes bubble up to parent
child.Add(42);  // Output: Change: 0, Path: Child.0
```

## Supported Frameworks

| Framework | Version |
|-----------|---------|
| .NET | 8.0, 10.0 (LTS) |

</div>
