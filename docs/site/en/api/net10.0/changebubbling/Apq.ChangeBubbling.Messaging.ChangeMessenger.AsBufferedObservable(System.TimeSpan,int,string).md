#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.AsBufferedObservable\(TimeSpan, int, string\) Method

获取指定环境的缓冲批量事件流。满足时间窗口或数量窗口任一条件即触发。

```csharp
public static System.IObservable<System.Collections.Generic.IList<Apq.ChangeBubbling.Abstractions.BubblingChange>> AsBufferedObservable(System.TimeSpan timeWindow, int countWindow, string envName="default");
```
#### Parameters

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.AsBufferedObservable(System.TimeSpan,int,string).timeWindow'></a>

`timeWindow` [System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.AsBufferedObservable(System.TimeSpan,int,string).countWindow'></a>

`countWindow` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.AsBufferedObservable(System.TimeSpan,int,string).envName'></a>

`envName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

#### Returns
[System\.IObservable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')[System\.Collections\.Generic\.IList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1 'System\.Collections\.Generic\.IList\`1')[BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1 'System\.Collections\.Generic\.IList\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')