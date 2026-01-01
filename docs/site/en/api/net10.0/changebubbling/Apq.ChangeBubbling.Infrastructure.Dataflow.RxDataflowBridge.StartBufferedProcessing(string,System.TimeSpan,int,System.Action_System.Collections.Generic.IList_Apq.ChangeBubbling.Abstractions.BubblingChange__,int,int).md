#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.Dataflow](Apq.ChangeBubbling.Infrastructure.Dataflow.md 'Apq\.ChangeBubbling\.Infrastructure\.Dataflow').[RxDataflowBridge](Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.md 'Apq\.ChangeBubbling\.Infrastructure\.Dataflow\.RxDataflowBridge')

## RxDataflowBridge\.StartBufferedProcessing\(string, TimeSpan, int, Action\<IList\<BubblingChange\>\>, int, int\) Method

订阅指定环境的缓冲事件流，并通过 Dataflow ActionBlock 按批处理。
返回 IDisposable，用于一次性清理订阅与管线。

```csharp
public static System.IDisposable StartBufferedProcessing(string envName, System.TimeSpan timeWindow, int countWindow, System.Action<System.Collections.Generic.IList<Apq.ChangeBubbling.Abstractions.BubblingChange>> batchHandler, int boundedCapacity=1024, int maxDegreeOfParallelism=1);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.StartBufferedProcessing(string,System.TimeSpan,int,System.Action_System.Collections.Generic.IList_Apq.ChangeBubbling.Abstractions.BubblingChange__,int,int).envName'></a>

`envName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.StartBufferedProcessing(string,System.TimeSpan,int,System.Action_System.Collections.Generic.IList_Apq.ChangeBubbling.Abstractions.BubblingChange__,int,int).timeWindow'></a>

`timeWindow` [System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')

<a name='Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.StartBufferedProcessing(string,System.TimeSpan,int,System.Action_System.Collections.Generic.IList_Apq.ChangeBubbling.Abstractions.BubblingChange__,int,int).countWindow'></a>

`countWindow` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.StartBufferedProcessing(string,System.TimeSpan,int,System.Action_System.Collections.Generic.IList_Apq.ChangeBubbling.Abstractions.BubblingChange__,int,int).batchHandler'></a>

`batchHandler` [System\.Action&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System\.Action\`1')[System\.Collections\.Generic\.IList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1 'System\.Collections\.Generic\.IList\`1')[BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1 'System\.Collections\.Generic\.IList\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 'System\.Action\`1')

<a name='Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.StartBufferedProcessing(string,System.TimeSpan,int,System.Action_System.Collections.Generic.IList_Apq.ChangeBubbling.Abstractions.BubblingChange__,int,int).boundedCapacity'></a>

`boundedCapacity` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.StartBufferedProcessing(string,System.TimeSpan,int,System.Action_System.Collections.Generic.IList_Apq.ChangeBubbling.Abstractions.BubblingChange__,int,int).maxDegreeOfParallelism'></a>

`maxDegreeOfParallelism` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

#### Returns
[System\.IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable 'System\.IDisposable')