using UnityEngine;

public class BatteryObject : Interactable
{
    private void Start()
    {
        objectName = "Battery";
    }

    public override void Interact()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Only interact if the item is a shovel
        if (item != null && item.itemName.Equals("Null"))
        {
            Destroy(gameObject);
        }
    }
}
