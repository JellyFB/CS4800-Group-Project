using System.Collections.Generic;
using UnityEngine;

public class DebrisObject : Interactable
{
    [SerializeField] private AudioClip debrisDug;

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
            
            // Play pickup audio using AudioManager
            AudioManager.instance.PlaySound(debrisDug);

            Destroy(gameObject.transform.root.gameObject);  
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

