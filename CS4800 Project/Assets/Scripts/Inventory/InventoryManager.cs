using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _inventory;
    [SerializeField] private int _numberOfInventorySlots = 5;
    private InventorySlot[] _slots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _slots = GetComponentsInChildren<InventorySlot>();

        if (_slots.Length != _numberOfInventorySlots)
            Debug.LogError("Number of inventory slots is wrong!");

        // Makes sure the amount of items within the inventory is less than or equal to the max.
        while (_inventory.Count > _numberOfInventorySlots)
        {
            _inventory.RemoveAt(_numberOfInventorySlots);
        }

        // Assigns the icons to each of the hotbar.

        // Makes sure the player is at hotbar slot 1 at the start of the game.
        SwitchSlot(1);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    // Checks for numeric input for hotbar switching.
    private void CheckInput()
    {
        for(int i = 1; i <= _numberOfInventorySlots; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                SwitchSlot(i);
            }
        }
    }

    // Switches the slot the player is currently on.
    // Should switch the model the player is holding and should highlight the hotbar slot the player is on.
    private void SwitchSlot(int slotNumber)
    {
        
    }

    // On pickup, adds the item to the hotbar.
    // Will be called by another method outside of class.
    private bool PickupItem(GameObject item)
    {
        for (int i = 0; i < _numberOfInventorySlots; i++)
        {
            if (i >= _numberOfInventorySlots)
            {
                _inventory.Add(item);
            }
            else if (_inventory[i] == null)
            {
                _inventory.RemoveAt(i);
                _inventory.Insert(i, item);
            }
        }

        return true;
    }

    // Removes item from the hotbar.
    // Will probably be called by another method outside of class.
    private void RemoveItem(int index)
    {
        _inventory.RemoveAt(index);
        _inventory.Insert(index, null);
    }
}
