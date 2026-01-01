#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Dataflow](Apq.ChangeBubbling.Infrastructure.Dataflow.md 'Apq\.ChangeBubbling\.Infrastructure\.Dataflow')

## RxDataflowBridge Class

将 Rx 的缓冲事件流桥接到 Dataflow 管线，既利用 Rx 的时间/数量窗口，又通过 Dataflow 的有界容量与并行度降低 GC 压力。

```csharp
public static class RxDataflowBridge
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; RxDataflowBridge

| Methods | |
| :--- | :--- |
| [StartBufferedProcessing\(string, TimeSpan, int, Action&lt;IList&lt;BubblingChange&gt;&gt;, int, int\)](Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.StartBufferedProcessing(string,System.TimeSpan,int,System.Action_System.Collections.Generic.IList_Apq.ChangeBubbling.Abstractions.BubblingChange__,int,int).md 'Apq\.ChangeBubbling\.Infrastructure\.Dataflow\.RxDataflowBridge\.StartBufferedProcessing\(string, System\.TimeSpan, int, System\.Action\<System\.Collections\.Generic\.IList\<Apq\.ChangeBubbling\.Abstractions\.BubblingChange\>\>, int, int\)') | 订阅指定环境的缓冲事件流，并通过 Dataflow ActionBlock 按批处理。 返回 IDisposable，用于一次性清理订阅与管线。 |
