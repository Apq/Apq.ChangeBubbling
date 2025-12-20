using System;
using System.Collections;
using System.Collections.Generic;

namespace Apq.ChangeBubbling.Snapshot;

/// <summary>
/// 多值容器（List/Dictionary）与快照的互转工具。
/// </summary>
public static class MultiValueSnapshotService
{
    public static MultiValueSnapshot ExportFromList(string name, IEnumerable list)
    {
        var snap = new MultiValueSnapshot { Name = name, Kind = "List" };
        foreach (var item in list)
        {
            snap.ListItems.Add(item);
        }
        return snap;
    }

    public static MultiValueSnapshot ExportFromDictionary(string name, IDictionary dict)
    {
        var snap = new MultiValueSnapshot { Name = name, Kind = "Dictionary" };
        foreach (DictionaryEntry kv in dict)
        {
            // 将 null 或字符串 "null" 统一视为空键
            var keyStr = kv.Key?.ToString();
            var key = (keyStr is null || string.Equals(keyStr, "null", StringComparison.OrdinalIgnoreCase))
                ? string.Empty
                : keyStr;
            snap.DictItems[key] = kv.Value;
        }
        return snap;
    }

    public static void ImportToList(IEnumerable targetList)
    {
        // 仅作为占位：不同集合类型的具体导入策略由调用方根据类型自行实现
        // 例如：IList.Add 或 ObservableCollection<T>.Add 等
    }
}


