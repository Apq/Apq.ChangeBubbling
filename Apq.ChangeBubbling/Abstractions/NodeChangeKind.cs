namespace Apq.ChangeBubbling.Abstractions;

/// <summary>
/// 表示节点或集合发生的变更类型。
/// </summary>
public enum NodeChangeKind
{
    PropertyUpdate,
    CollectionAdd,
    CollectionRemove,
    CollectionReplace,
    CollectionMove,
    CollectionReset
}


