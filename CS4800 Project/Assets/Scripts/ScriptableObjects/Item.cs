using Unity.VisualScripting;
using UnityEngine;

public class Item
{
    public string itemName;

    // Prefab to be used to render item in front of player when the hotbar with the item is selected.
    public Object prefab;

    // Sprite to be used in the hotbar
    public Sprite sprite;

    public void SetItemInfo(ItemInfo itemInfo)
    {
        itemName = itemInfo.itemName;
        prefab = itemInfo.prefab;
        sprite = itemInfo.sprite;
    }
}
