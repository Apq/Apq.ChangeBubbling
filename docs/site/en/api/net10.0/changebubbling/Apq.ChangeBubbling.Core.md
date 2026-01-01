#### [Apq\.ChangeBubbling](index.md 'index')

## Apq\.ChangeBubbling\.Core Namespace

| Classes | |
| :--- | :--- |
| [ChangeNodeBase](Apq.ChangeBubbling.Core.ChangeNodeBase.md 'Apq\.ChangeBubbling\.Core\.ChangeNodeBase') | 提供父子管理与冒泡转译的基类。派生类可借助 Fody 自动织入属性变更。 使用弱事件订阅避免内存泄漏。线程安全。 |
| [WeakEventSubscription](Apq.ChangeBubbling.Core.WeakEventSubscription.md 'Apq\.ChangeBubbling\.Core\.WeakEventSubscription') | 弱事件订阅管理器，避免父节点持有子节点的强引用导致内存泄漏。 使用缓存的强引用优化高频事件处理性能。 检查间隔使用自适应策略：高频事件时增加间隔，低频时减少间隔。 |

| Interfaces | |
| :--- | :--- |
| [IChangeNode](Apq.ChangeBubbling.Core.IChangeNode.md 'Apq\.ChangeBubbling\.Core\.IChangeNode') | 定义具有父子关系、支持冒泡通知的节点接口。 |
