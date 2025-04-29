using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemInfo : ScriptableObject
{
    public enum ItemType {Tool, Debris, Battery};

    public ItemType itemType;

    public string itemName;

    // Prefab to be used to render item in front of player when the hotbar with the item is selected.
    public Object prefab;

    // Sprite to be used in the hotbar (not currently implemented)
    public Sprite sprite;
}
