using Unity.VisualScripting;
using UnityEngine;

public class DisposalArea : Interactable
{
    public override void Interact()
    {
        // // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Check for item in hand
        if (item != null && item is BatteryItem batteryItem)
        {
            if (batteryItem.voltage.Equals("low"))
            {
                // Conditions if volt = low AND temp = low
                if(batteryItem.temperature.Equals("low")) 
                {
                    // Correct placement of battery and will just increment task
                    // If player placed rag around it and put it in the barrel it will be returned to them
                    if (batteryItem.isWrapped == true)
                    {
                        PlayerManager.instance.inventoryManager.PickupItem(batteryItem.rag);
                    }
                    TaskManager.instance.IncrementTask(TaskTypes.DisposeBatteries);
                    PlayerManager.instance.inventoryManager.RemoveCurrentItem();
                }
                // Conditions if volt = low AND temp = high
                else
                {
                    // If the player places it in this area it will mark it as a "task fail" and 
                        // will notify the user at the end of the simulation
                    TaskManager.instance.FailedTask(TaskTypes.DisposeBatteries);
                    // If player placed rag around it and put it in the barrel it will be returned to them
                    if (batteryItem.isWrapped == true)
                    {
                        PlayerManager.instance.inventoryManager.PickupItem(batteryItem.rag);
                    }
                    TaskManager.instance.IncrementTask(TaskTypes.DisposeBatteries);
                    PlayerManager.instance.inventoryManager.RemoveCurrentItem();
                }
                    
            }

            if (batteryItem.voltage.Equals("high"))
            {
                // Conditions if volt = high AND temp = high/low
                if(batteryItem.temperature.Equals("high") || batteryItem.temperature.Equals("low")) 
                {
                    // Correct placement of battery and will return user the rag item
                    if (batteryItem.isWrapped == true)
                    {
                        PlayerManager.instance.inventoryManager.PickupItem(batteryItem.rag);
                    }
                    // Incorrect placement of battery *missing the rag* 
                    else
                    {
                        // Returns rag but marks it as a fail
                        TaskManager.instance.FailedTask(TaskTypes.DisposeBatteries);
                        PlayerManager.instance.inventoryManager.PickupItem(batteryItem.rag);
                    }
                    // Both choices will result in the increment of the task and removal of item
                    TaskManager.instance.IncrementTask(TaskTypes.DisposeBatteries);
                    PlayerManager.instance.inventoryManager.RemoveCurrentItem();
                }
            }

            // For testing
            TaskManager.instance.DisplayFail(TaskTypes.DisposeBatteries);

        }
    }

    public override string OnHover()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Check if the currently held item is a battery
        if (item != null && item is BatteryItem batteryItem)
        {
            return $"Press [E] to Deposit � {batteryItem.itemName}";
        }
        return "Deposit batteries here!";
    }
}
