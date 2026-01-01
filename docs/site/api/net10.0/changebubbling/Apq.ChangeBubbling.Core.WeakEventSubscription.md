#### [Apq\.ChangeBubbling](index.md 'index')
### [Apq\.ChangeBubbling\.Core](Apq.ChangeBubbling.Core.md 'Apq\.ChangeBubbling\.Core')

## WeakEventSubscription Class

弱事件订阅管理器，避免父节点持有子节点的强引用导致内存泄漏。
使用缓存的强引用优化高频事件处理性能。
检查间隔使用自适应策略：高频事件时增加间隔，低频时减少间隔。

```csharp
internal sealed class WeakEventSubscription : System.IDisposable
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; WeakEventSubscription

Implements [System\.IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable 'System\.IDisposable')

| Methods | |
| :--- | :--- |
| [AdaptIntervalOnHit\(\)](Apq.ChangeBubbling.Core.WeakEventSubscription.AdaptIntervalOnHit().md 'Apq\.ChangeBubbling\.Core\.WeakEventSubscription\.AdaptIntervalOnHit\(\)') | 缓存命中时调整间隔（增加间隔，减少检查频率）。 |
| [AdaptIntervalOnMiss\(\)](Apq.ChangeBubbling.Core.WeakEventSubscription.AdaptIntervalOnMiss().md 'Apq\.ChangeBubbling\.Core\.WeakEventSubscription\.AdaptIntervalOnMiss\(\)') | 缓存未命中时调整间隔（减少间隔，增加检查频率）。 |
