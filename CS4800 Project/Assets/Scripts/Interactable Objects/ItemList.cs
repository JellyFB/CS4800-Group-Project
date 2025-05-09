using System.Collections.Generic;
using UnityEngine;

public static class ItemList
{
    public static Dictionary<string, ItemInfo> items = new Dictionary<string, ItemInfo>()
    {
        {"Shovel", Resources.Load<ItemInfo>("Scriptable Objects/Tools/Shovel")},
        {"Crowbar", Resources.Load<ItemInfo>("Scriptable Objects/Tools/Crowbar")},
        {"Drill", Resources.Load<ItemInfo>("Scriptable Objects/Tools/Drill")},
        {"Multimeter", Resources.Load<ItemInfo>("Scriptable Objects/Tools/Multimeter")},
        {"Rag", Resources.Load<ItemInfo>("Scriptable Objects/Tools/Rag")},
        {"Screwdriver", Resources.Load<ItemInfo>("Scriptable Objects/Tools/Screwdriver")},
        {"Temp Probe", Resources.Load<ItemInfo>("Scriptable Objects/Tools/TempProbe")},
    };

    // Returns item depending on itemName
    public static Item GetItem(string itemName)
    {
        Item item = new Item();
        item.SetItemInfo(items[itemName]);

        return item;
    }
}
