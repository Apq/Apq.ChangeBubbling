#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.AsThrottledObservable\(Nullable\<TimeSpan\>, string\) Method

获取指定环境的节流事件流（默认 50ms）。

```csharp
public static System.IObservable<Apq.ChangeBubbling.Abstractions.BubblingChange> AsThrottledObservable(System.Nullable<System.TimeSpan> dueTime=null, string envName="default");
```
#### Parameters

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.AsThrottledObservable(System.Nullable_System.TimeSpan_,string).dueTime'></a>

`dueTime` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.AsThrottledObservable(System.Nullable_System.TimeSpan_,string).envName'></a>

`envName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

#### Returns
[System\.IObservable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')[BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')