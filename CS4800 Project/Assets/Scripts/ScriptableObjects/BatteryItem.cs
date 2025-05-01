using System;
using UnityEngine;

public class BatteryItem : Item
{
    public String temperature;
    public String voltage;
    
    // Used when the battery is wrapped in a rag
    public bool isWrapped = false;
    public Item rag;

    // Sets battery-specific fields
    public void SetBatteryInfo(String temperature, String voltage)
    {
        this.temperature = temperature;
        this.voltage = voltage;
    }

    public void WrapBattery(Item rag)
    {
        this.rag = rag;
        isWrapped = true;
        itemName = "Wrapped " + itemName;
    }
}
