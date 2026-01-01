#### [Apq\.ChangeBubbling](index.md 'index')

## Apq\.ChangeBubbling\.Infrastructure\.Dataflow Namespace

| Classes | |
| :--- | :--- |
| [ChangeDataflowPipeline](Apq.ChangeBubbling.Infrastructure.Dataflow.ChangeDataflowPipeline.md 'Apq\.ChangeBubbling\.Infrastructure\.Dataflow\.ChangeDataflowPipeline') | 基于 Dataflow 的冒泡事件背压管线：缓冲 \+ 处理，支持可配置并行度与容量。 |
| [RxDataflowBridge](Apq.ChangeBubbling.Infrastructure.Dataflow.RxDataflowBridge.md 'Apq\.ChangeBubbling\.Infrastructure\.Dataflow\.RxDataflowBridge') | 将 Rx 的缓冲事件流桥接到 Dataflow 管线，既利用 Rx 的时间/数量窗口，又通过 Dataflow 的有界容量与并行度降低 GC 压力。 |
