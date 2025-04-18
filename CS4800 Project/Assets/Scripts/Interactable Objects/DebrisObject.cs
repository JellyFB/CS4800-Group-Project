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
        // Check if the player is currently holding a shovel
        if (PlayerManager.instance.inventoryManager.GetCurrentHeldItem().itemName.Equals("Shovel"))
        {
            TaskManager.instance.UpdateTask(TaskTypes.RemoveDebris);
            Destroy(gameObject);
        }
    }

    public override string OnHover()
    {
        if (PlayerManager.instance.inventoryManager.GetCurrentHeldItem().itemName.Equals("Shovel"))
            return base.OnHover();
        else
            return $"Need shovel to remove {objectName}!";
    }
}
