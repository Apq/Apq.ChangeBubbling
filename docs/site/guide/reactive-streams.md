# Rx 响应式流

Apq.ChangeBubbling 深度集成 System.Reactive，提供强大的响应式编程能力。

## 启用 Rx 流

```csharp
using Apq.ChangeBubbling.Messaging;

// 启用 Rx 流支持
ChangeMessenger.EnableRxStream = true;
```

## 基本订阅

```csharp
using System.Reactive.Linq;

// 订阅所有变更
ChangeMessenger.AsObservable()
    .Subscribe(change => Console.WriteLine(change));

// 订阅指定环境
ChangeMessenger.AsObservable("ui")
    .Subscribe(change => UpdateUI(change));
```

## 过滤操作

### 按变更类型过滤

```csharp
ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .Subscribe(c => Console.WriteLine($"Added: {c.NewValue}"));
```

### 按节点名称过滤

```csharp
ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Users")
    .Subscribe(c => HandleUserChange(c));
```

### 按属性名称过滤

```csharp
ChangeMessenger.AsObservable()
    .Where(c => c.PropertyName == "Name" || c.PropertyName == "Email")
    .Subscribe(c => ValidateField(c));
```

## 节流与防抖

### 节流 (Throttle)

限制事件频率，丢弃中间事件：

```csharp
ChangeMessenger.AsObservable()
    .Throttle(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => SaveChanges(c));

// 或使用内置方法
ChangeMessenger.AsThrottledObservable(TimeSpan.FromMilliseconds(100))
    .Subscribe(c => SaveChanges(c));
```

### 防抖 (Debounce)

等待事件停止后再处理：

```csharp
ChangeMessenger.AsObservable()
    .Debounce(TimeSpan.FromMilliseconds(300))
    .Subscribe(c => SearchAsync(c.NewValue?.ToString()));
```

### 采样 (Sample)

定期采样最新事件：

```csharp
ChangeMessenger.AsObservable()
    .Sample(TimeSpan.FromSeconds(1))
    .Subscribe(c => UpdateStatus(c));
```

## 缓冲操作

### 时间缓冲

```csharp
ChangeMessenger.AsObservable()
    .Buffer(TimeSpan.FromSeconds(1))
    .Where(batch => batch.Count > 0)
    .Subscribe(batch => ProcessBatch(batch));
```

### 数量缓冲

```csharp
ChangeMessenger.AsObservable()
    .Buffer(10)
    .Subscribe(batch => ProcessBatch(batch));
```

### 滑动窗口

```csharp
ChangeMessenger.AsObservable()
    .Window(TimeSpan.FromSeconds(5))
    .SelectMany(window => window.ToList())
    .Subscribe(changes => AnalyzeWindow(changes));
```

## 转换操作

### Select

```csharp
ChangeMessenger.AsObservable()
    .Select(c => new ChangeDto
    {
        Type = c.Kind.ToString(),
        Node = c.NodeName,
        Value = c.NewValue
    })
    .Subscribe(dto => SendToServer(dto));
```

### SelectMany

```csharp
ChangeMessenger.AsObservable()
    .Where(c => c.Kind == NodeChangeKind.CollectionAdd)
    .SelectMany(c => ValidateAsync(c.NewValue))
    .Subscribe(result => HandleValidation(result));
```

## 组合操作

### Merge

```csharp
var userChanges = ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Users");

var settingChanges = ChangeMessenger.AsObservable()
    .Where(c => c.NodeName == "Settings");

userChanges.Merge(settingChanges)
    .Subscribe(c => LogChange(c));
```

### CombineLatest

```csharp
var nameChanges = ChangeMessenger.AsObservable()
    .Where(c => c.PropertyName == "Name");

var emailChanges = ChangeMessenger.AsObservable()
    .Where(c => c.PropertyName == "Email");

nameChanges.CombineLatest(emailChanges, (name, email) => new { name, email })
    .Subscribe(x => UpdateProfile(x.name, x.email));
```

## 错误处理

```csharp
ChangeMessenger.AsObservable()
    .Do(c => Console.WriteLine($"Processing: {c}"))
    .Catch<BubblingChange, Exception>(ex =>
    {
        Console.WriteLine($"Error: {ex.Message}");
        return Observable.Empty<BubblingChange>();
    })
    .Retry(3)
    .Subscribe(c => ProcessChange(c));
```

## 资源管理

```csharp
// 使用 CompositeDisposable 管理多个订阅
var disposables = new CompositeDisposable();

disposables.Add(
    ChangeMessenger.AsObservable()
        .Where(c => c.NodeName == "Users")
        .Subscribe(c => HandleUserChange(c))
);

disposables.Add(
    ChangeMessenger.AsObservable()
        .Where(c => c.NodeName == "Settings")
        .Subscribe(c => HandleSettingChange(c))
);

// 一次性取消所有订阅
disposables.Dispose();
```
