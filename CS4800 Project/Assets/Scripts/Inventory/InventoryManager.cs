using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<Item> _inventory;
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

    // Checks for numeric input for hotbar switching.
    private void CheckInput()
    {
        for(int i = 1; i <= _slots.Length; i++)
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
    // Will be called by another method outside of class.
    public bool PickupItem(Item item)
    {
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

        return false;
    }

    // Removes item from the hotbar.
    // Will probably be called by another method outside of class.
    public void RemoveItem(int index)
    {
        _inventory.RemoveAt(index);
        _inventory.Insert(index, null);
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

    private void PutItemOnPocket(Item item)
    {
        if (item == null || item.prefab == null)
            return;

        Object instantiatedItem = Instantiate(item.prefab, _playerPocket.transform);
        _currentItemHeld = instantiatedItem;
    }
}
