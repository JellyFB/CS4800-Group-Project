using System;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class BatteryObject : Interactable
{
    [Header("Temporary")]
    [SerializeField] private String[] _status = {"high", "low"};
    [SerializeField] private String _temp;
    [SerializeField] private bool _voltage;

    // Identifier for the specific object in game 
    private void Start()
    {
        objectName = "Battery";
        _temp = _status[UnityEngine.Random.Range(0,2)];
    }

    // Interact behavior for battery
    public override void Interact()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Interactions for the object - not implemented yet
        if (item != null && item.itemName.Equals("Null"))
        {
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
            return base.OnHover();
        
        // Checks for specific items on hand
        if (item.itemName.Equals("Multimeter"))
        {
            return $"Voltage: {_voltage}";
        }
        else if (item.itemName.Equals("Temperature Probe"))
        {
            return $"Temperature: {_temp}";
        }
        else
        {
            return base.OnHover();
        }
    }
}
