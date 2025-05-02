using UnityEngine;

public class BarrelDisposalArea : Interactable
{
    public override void Interact()
    {
        // Get player item currently in inventory
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Check for item in hand
        if (item != null && item is BatteryItem batteryItem)
        {
            if (batteryItem.voltage.Equals("low"))
            {
                // Conditions if volt = low AND temp = high
                if(batteryItem.temperature.Equals("high")) 
                {
                    PlayerManager.instance.inventoryManager.RemoveCurrentItem();
                }
                // Conditions if volt = low AND temp = low
                else
                {
                    return;
                }
            }

            if (batteryItem.voltage.Equals("high"))
            {
                // Conditions if volt = high AND temp = high
                if(batteryItem.temperature.Equals("high")) 
                {
                    return;
                }
                // Conditions if volt = high AND temp = low
                else
                {
                    return;
                }
            }

        }

    }

    public override string OnHover()
    {
        // Get player item currently in inventory
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Check for item in hand
        if (item != null && item is BatteryItem batteryItem)
        {
            if (batteryItem.voltage.Equals("low"))
            {
                // Conditions if volt = low AND temp = high
                if(batteryItem.temperature.Equals("high")) 
                {
                    return $"Press [E] to Deposit {batteryItem.itemName}";
                }
                // Conditions if volt = low AND temp = low
                else
                {
                    return $"Battery cannot be deposited here.";
                }
            }
            else 
            {
                // Conditions if volt = high AND temp = high OR low
                return $"Battery cannot be deposited here.";
            }

        }
        return "Deposit batteries here!";
    }

}
