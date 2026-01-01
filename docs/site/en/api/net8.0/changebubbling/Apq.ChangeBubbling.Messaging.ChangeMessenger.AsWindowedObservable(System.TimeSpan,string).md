#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Messaging](Apq.ChangeBubbling.Messaging.md 'Apq\.ChangeBubbling\.Messaging').[ChangeMessenger](Apq.ChangeBubbling.Messaging.ChangeMessenger.md 'Apq\.ChangeBubbling\.Messaging\.ChangeMessenger')

## ChangeMessenger\.AsWindowedObservable\(TimeSpan, string\) Method

获取指定环境的时间窗口切片事件流（Window）。

```csharp
public static System.IObservable<System.IObservable<Apq.ChangeBubbling.Abstractions.BubblingChange>> AsWindowedObservable(System.TimeSpan timeWindow, string envName="default");
```
#### Parameters

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.AsWindowedObservable(System.TimeSpan,string).timeWindow'></a>

`timeWindow` [System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')

<a name='Apq.ChangeBubbling.Messaging.ChangeMessenger.AsWindowedObservable(System.TimeSpan,string).envName'></a>

`envName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

#### Returns
[System\.IObservable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')[System\.IObservable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')[BubblingChange](Apq.ChangeBubbling.Abstractions.BubblingChange.md 'Apq\.ChangeBubbling\.Abstractions\.BubblingChange')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1 'System\.IObservable\`1')