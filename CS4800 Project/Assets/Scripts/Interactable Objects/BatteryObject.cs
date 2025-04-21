using System;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class BatteryObject : Interactable
{
    private String[] status = {"high", "low"};
    private String temp;
    private bool voltage;
    // Identifier for the specific object in game 
    private void Start()
    {
        objectName = "Battery";
        temp = status[UnityEngine.Random.Range(0,2)];
    }

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
}
