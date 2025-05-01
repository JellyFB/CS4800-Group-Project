using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<Item> _inventory = new List<Item>();
    [SerializeField] private GameObject _playerPocket;

    private InventorySlot[] _slots;
    private int _currentSlot = 1;
    private Object _currentItemHeld;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    
    {
        _slots = GetComponentsInChildren<InventorySlot>();


        // Makes sure the amount of items within the inventory is less than or equal to the max.
        while (_inventory.Count > _slots.Length)
        {
            _inventory.RemoveAt(_slots.Length);
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

    
    private void CheckInput()
    {
        int i = 1;
        // Checks for numeric input for hotbar switching.
        for (i = 1; i <= _slots.Length; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                SwitchSlot(i);
            }
        }

        // Also checks for mouse wheel scroll
        if (Input.mouseScrollDelta.y > 0)
        {
            i = _currentSlot + 1;
            if (i > _slots.Length)
                i = 1;
            SwitchSlot(i);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            i = _currentSlot - 1;
            if (i < 1)
                i = _slots.Length;
            SwitchSlot(i);
        }
    }

    // Switches the slot the player is currently on.
    // Should switch the model the player is holding and should highlight the hotbar slot the player is on.
    private void SwitchSlot(int slotNumber)
    {
        // Deselects previous selected slot and removes the item in hand.
        _slots[_currentSlot - 1].Deselect();
        if (_currentItemHeld != null)
        {
            Destroy(_currentItemHeld);
            _currentItemHeld = null;
        }

        // Changes slot to given slot index and puts any item from the slot to the pocket.
        _currentSlot = slotNumber;

        _slots[_currentSlot - 1].Select();
        PutItemOnPocket(_slots[_currentSlot - 1].GetItem());
    }

    // On pickup, adds the item to the hotbar.
    public bool PickupItem(Item item)
    {
        // Returns when inventory is full
        if (IsFull())
            return false;

        // Finds the nearest empty slot
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i >= _inventory.Count)
            {
                AddItemToSlot(item, i);

                _inventory.Add(item);
                return true;
            }
            else if (_inventory[i] == null)
            {
                AddItemToSlot(item, i);

                _inventory.RemoveAt(i);
                _inventory.Insert(i, item);
                return true;
            }
        }

        // If it reaches here, then the slots are full
        return false;
    }

    // Removes currently held item from the hotbar.
    public bool RemoveCurrentItem()
    {
        // Returns false if the current slot is empty
        if (_inventory.Count < _currentSlot || _inventory[_currentSlot - 1] == null)
            return false;

        // Empties the current slot
        _slots[_currentSlot - 1].SetItem(null);
        _inventory.RemoveAt(_currentSlot - 1);
        _inventory.Insert(_currentSlot - 1, null);

        // Makes sure the character is no longer holding the item
        SwitchSlot(_currentSlot);

        return true;
    }

    // Gets current held item.
    public Item GetCurrentHeldItem()
    {
        if (_currentSlot <= _inventory.Count)
            return _inventory[_currentSlot - 1];
        else
            return null;
    }

    // Adds item to given slot.
    private void AddItemToSlot(Item item, int slotNumber)
    {
        _slots[slotNumber].SetItem(item);

        if (slotNumber == _currentSlot - 1)
        {
            PutItemOnPocket(item);
        }
    }

    // Sets the item on hand or pocket.
    private void PutItemOnPocket(Item item)
    {
        if (item == null || item.prefab == null)
            return;

        Object instantiatedItem = Instantiate(item.prefab, _playerPocket.transform);
        _currentItemHeld = instantiatedItem;
    }

    // Checks if the inventory slot is full.
    public bool IsFull()
    {
        // Finds empty slots
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i >= _inventory.Count || _inventory[i] == null)
            {
                return false;
            }
        }

        return true;
    }
}

