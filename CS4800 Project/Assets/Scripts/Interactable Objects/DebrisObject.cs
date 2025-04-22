using System.Collections.Generic;
using UnityEngine;

public class DebrisObject : Interactable
{
    private void Start()
    {
        objectName = "Debris";
    }

    public override void Interact()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Only interact if the item is a shovel
        if (item != null && item.itemName.Equals("Shovel"))
        {
            TaskManager.instance.IncrementTask(TaskTypes.RemoveDebris);
            Destroy(gameObject);
        }
    }

    public override string OnHover()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Change text if player is holding a shovel
        if (item != null && item.itemName.Equals("Shovel"))
            return base.OnHover();
        else
            return $"Need shovel to remove {objectName}!";
    }
}

