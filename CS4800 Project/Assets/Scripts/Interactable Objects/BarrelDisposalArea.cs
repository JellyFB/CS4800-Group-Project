using UnityEngine;

public class BarrelDisposalArea : Interactable
{
    public override void Interact()
    {
        // Get player item currently in inventory
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Check for item in hand is a battery
        if (item != null && item is BatteryItem batteryItem)
        {

            if (batteryItem.voltage.Equals("low"))
            {
                // Conditions if volt = low AND temp = low
                if(batteryItem.temperature.Equals("low")) 
                {
                    // Incorrect placement of battery
                    // If the player places it in this area it will mark it as a "task fail" and 
                    // will notify the user at the end of the simulation
                    TaskManager.instance.DisplayFail(TaskTypes.DisposeBatteries);
                    // Correct placement of battery and will just increment task
                }
                // Conditions if volt = low AND temp = high
                    // Only correct instance to put in barrel for battery
                // If player placed rag around it and put it in the barrel it will be returned to them
                if (batteryItem.isWrapped == true)
                {
                    PlayerManager.instance.inventoryManager.PickupItem(batteryItem.rag);
                }
                TaskManager.instance.IncrementTask(TaskTypes.DisposeBatteries);
                PlayerManager.instance.inventoryManager.RemoveCurrentItem();
            }

            if (batteryItem.voltage.Equals("high"))
            {
                // Conditions if volt = high AND temp = high/low
                    // Incorrect placement of battery
                // If player placed rag around it and put it in the barrel it will be returned to them
                if (batteryItem.isWrapped == true)
                {
                    PlayerManager.instance.inventoryManager.PickupItem(batteryItem.rag);
                }
                TaskManager.instance.DisplayFail(TaskTypes.DisposeBatteries);
                TaskManager.instance.IncrementTask(TaskTypes.DisposeBatteries);
                PlayerManager.instance.inventoryManager.RemoveCurrentItem();

            }

            // For testing
            TaskManager.instance.DisplayFail(TaskTypes.DisposeBatteries);
        }
    }

    public override string OnHover()
    {
        // Get player item currently in inventory
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Check for item in hand is a battery
        if (item != null && item is BatteryItem batteryItem)
        {
           return $"Press [E] to Deposit {batteryItem.itemName}";
        }
        return "Deposit batteries here!";
    }

}
