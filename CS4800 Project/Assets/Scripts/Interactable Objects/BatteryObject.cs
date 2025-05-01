using System;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class BatteryObject : Interactable
{
    [Header("Battery Attributes")]
    // Allows for batteries to be picked up, not just deleted
    public bool isPickable = false;
    // Allows for batteries to be interacted with without the need for a crowbar
    public bool needCrowbar = true;

    [Header("Battery Info")]
    // Holds info about the battery
    [SerializeField] private ItemInfo batteryInfo;

    private String[] _status = {"high", "low"};
    [HideInInspector] public String _temperature;
    [HideInInspector] public String _voltage;

    // Identifier for the specific object in game 
    private void Start()
    {
        objectName = "Battery";
        _temperature = _status[UnityEngine.Random.Range(0,2)];
        _voltage = _status[UnityEngine.Random.Range(0,2)];
    }

    // Interact behavior for battery
    public override void Interact()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Condition 1: No need to use crowbar to pick up item OR
        // Condition 2: Need to use crowbar and have crowbar on hand
        if (!needCrowbar || (needCrowbar && item != null && item.itemName.Equals("Crowbar")))
        {
            // Picks the battery up if it is pickable
            if (isPickable)
            {
                // Provide battery info to the inventory
                // Returns if the pick up failed (due to inventory being full or something else)
                if (!PlayerManager.instance.inventoryManager.PickupItem(Pick(item)))
                    return;
            }

            TaskManager.instance.IncrementTask(TaskTypes.RemoveBattery);
            Destroy(gameObject);
        }
    }

    // Triggers when hovering over item
    public override string OnHover()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Set default text
        string defaultHoverText = needCrowbar ? $"Need crowbar to remove {objectName}!" : base.OnHover();

        // Checks if there is no item on hand
        if (item == null)
            return defaultHoverText;

        // Checks for specific items on hand
        switch (item.itemName)
        {
            case "Multimeter":
                return $"Voltage: {_voltage}";
            case "Temperature Probe":
                return $"Temperature: {_temperature}";
            case "Crowbar":
                return base.OnHover();
            case "Rag":
                return $"Press [E] to wrap {objectName}!";
            default:
                return defaultHoverText;
        }
    }

    private Item Pick(Item currentlyHeldItem)
    {
        // Create item using the itemInfo
        BatteryItem item = new BatteryItem();
        item.SetItemInfo(batteryInfo);
        item.SetBatteryInfo(_temperature, _voltage);

        // Wraps battery if current item is a rag
        if (currentlyHeldItem != null && currentlyHeldItem.itemName.Equals("Rag"))
        {
            PlayerManager.instance.inventoryManager.RemoveCurrentItem();
            item.WrapBattery(currentlyHeldItem);
        }
           
        return item;
    }
}

