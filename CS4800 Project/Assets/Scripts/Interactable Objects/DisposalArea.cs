using Unity.VisualScripting;
using UnityEngine;

public class DisposalArea : Interactable
{
    public override void Interact()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Remove current item if it is a battery
        if (item != null && item is BatteryItem batteryItem)
        {
            // Checks for specific circumstances where battery cannot be deposited in the area
            if (batteryItem.voltage.Equals("low") && batteryItem.temperature.Equals("high"))
            {
                return;
            }

            PlayerManager.instance.inventoryManager.RemoveCurrentItem();
        }
    }

    public override string OnHover()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Check if the currently held item is a battery
        if (item != null && item is BatteryItem batteryItem)
        {
            // Checks for specific circumstances where battery cannot be deposited in the area
            if (batteryItem.voltage.Equals("low") && batteryItem.temperature.Equals("high"))
            {
                return $"Battery cannot be deposited here.";
            }

            return $"Press [E] to Deposit — {batteryItem.itemName}";
        }

        return "Deposit batteries here!";
    }
}
