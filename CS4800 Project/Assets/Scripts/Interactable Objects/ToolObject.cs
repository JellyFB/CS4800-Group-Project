using UnityEngine;

public class ToolObject : Interactable
{
    [SerializeField] private ItemInfo toolInfo;
    public bool isPickable = true;

    private void Start()
    {
        // Makes sure the item name of the tool is the one showing in the feedback text
        objectName = toolInfo.itemName;
    }

    // On interact behavior of objects
    public override void Interact()
    {
        // Returns if the item is not pickable
        if (!isPickable)
            return;

        // Provide tool info to the inventory
        // Returns if the pick up failed (due to full inventory or something)
        if (!PlayerManager.instance.inventoryManager.PickupItem(Pick()))
            return;

        // Hides GameObject
        gameObject.SetActive(false);
    }

    // Provide tool info to the inventory
    private Item Pick()
    {
        TaskManager.instance.IncrementTask(TaskTypes.GetTools);

        // Create item using the itemInfo
        Item item = new Item();
        item.SetItemInfo(toolInfo);
        
        return item;
    }
}

