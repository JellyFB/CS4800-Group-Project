using System;
using UnityEngine;

public class BatteryItem : Item
{
    public String _temperature;
    public String _voltage;

    // Sets battery-specific fields
    public void SetBatteryInfo(String temperature, String voltage)
    {
        _temperature = temperature;
        _voltage = voltage;
    }
}
