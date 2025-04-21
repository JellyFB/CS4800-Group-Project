using System;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class BatteryObject : Interactable
{
    private String[] status = {"high", "low"};
    private String temp;
    private String voltage;
    // Identifier for the specific object in game 
    private void Start()
    {
        objectName = "Battery";
        temp = status[UnityEngine.Random.Range(0,2)];
        voltage = status[UnityEngine.Random.Range(0,2)];
    }

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

    public override string OnHover()
    {
        // Get player currently in hand/pocket
        Item item = PlayerManager.instance.inventoryManager.GetCurrentHeldItem();

        // Change text if player is holding a shovel
        if (item != null && item.itemName.Equals("Crowbar"))
            return base.OnHover();
        else
            return $"Need crowbar to remove {objectName}!";
    }
}
