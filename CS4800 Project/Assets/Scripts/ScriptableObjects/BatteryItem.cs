using System;
using UnityEngine;

public class BatteryItem : Item
{
    public String temperature;
    public String voltage;

    // Sets battery-specific fields
    public void SetBatteryInfo(String temperature, String voltage)
    {
        this.temperature = temperature;
        this.voltage = voltage;
    }
}
