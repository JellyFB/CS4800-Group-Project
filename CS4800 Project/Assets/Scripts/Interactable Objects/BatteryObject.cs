using System;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class BatteryObject : Interactable
{
    public bool isPickable = false;
    [SerializeField] private ItemInfo batteryInfo;

    private String[] _status = {"high", "low"};
    private String _temperature;
    private String _voltage;
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

        // Interactions for the object - not implemented yet
        if (item != null && item.itemName.Equals("Crowbar"))
        {
            TaskManager.instance.IncrementTask(TaskTypes.RemoveBattery);
            Destroy(gameObject);
        }
    }

    // Triggers when hovering over item
    public override string OnHover()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Checks if there is no item on hand
        if (item == null)
            return $"Need crowbar to remove {objectName}!";

        // Checks for specific items on hand
        switch (item.itemName)
        {
            case "Multimeter":
                return $"Voltage: {_voltage}";
            case "Temperature Probe":
                return $"Temperature: {_temperature}";
            case "Crowbar":
                return base.OnHover(); 
            default:
                return $"Need crowbar to remove {objectName}!";
        }
    }
}

