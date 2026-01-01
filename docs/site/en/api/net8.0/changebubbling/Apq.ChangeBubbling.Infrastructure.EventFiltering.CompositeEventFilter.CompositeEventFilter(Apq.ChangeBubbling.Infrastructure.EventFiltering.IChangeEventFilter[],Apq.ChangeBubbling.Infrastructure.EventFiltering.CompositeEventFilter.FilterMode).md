#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Infrastructure\.EventFiltering](Apq.ChangeBubbling.Infrastructure.EventFiltering.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering').[CompositeEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter')

## CompositeEventFilter\(IChangeEventFilter\[\], FilterMode\) Constructor

创建组合过滤器。

```csharp
public CompositeEventFilter(Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter[] filters, Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.FilterMode mode=Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.FilterMode.All);
```
#### Parameters

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.CompositeEventFilter(Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter[],Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.FilterMode).filters'></a>

`filters` [IChangeEventFilter](Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.IChangeEventFilter')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

过滤器列表

<a name='Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.CompositeEventFilter(Apq.ChangeBubbling.Infrastructure.EventFiltering.IChangeEventFilter[],Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.FilterMode).mode'></a>

`mode` [FilterMode](Apq.ChangeBubbling.Infrastructure.EventFiltering.CompositeEventFilter.FilterMode.md 'Apq\.ChangeBubbling\.Infrastructure\.EventFiltering\.CompositeEventFilter\.FilterMode')

过滤模式