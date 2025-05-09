using System.IO;
using System;
using UnityEngine;
using Unity.VisualScripting;
using NUnit.Framework;
using System.Collections.Generic;

public class SaveHandler
{
    FileDataHandler<SaveData> _dataHandler;

    public SaveHandler()
    {
        String path = Path.Combine(Application.persistentDataPath, "Saves");
        _dataHandler = new FileDataHandler<SaveData>(path, null);
    }

    // Updates the filename depending on the username and slot
    public void ChangeFilename(int slot)
    {
        // Change filename
        string username = GameManager.instance.currentUsername;
        _dataHandler.ChangeFilename($"{username}{slot}.json");
    }

    // Returns the save data depending on the slot
    public SaveData GetSaveData(int slot)
    {
        ChangeFilename(slot);
        
        // Get save data
        SaveData save = _dataHandler.Load();

        return save;
    }

    public void WriteSaveData(int slot)
    {
        ChangeFilename(slot);

        // Write to new save data
        SaveData save = new SaveData();

        // All basic information from level
        save.username = GameManager.instance.currentUsername;
        save.levelNumber = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        save.numberOfTasks = TaskManager.instance.TaskCount();
        save.completedTasks = TaskManager.instance.FinishedTaskCount();
        save.gameTime = GameManager.instance.GetLevelTime();
        save.playerPosition = PlayerManager.instance.player.transform.position;

        // Saving all the remaining objects the user has not interacted with
        GameObject[] savableDebris = GameObject.FindGameObjectsWithTag("Debris");
        save.remainingDebris = savableDebris.Length;
        GameObject[] savableBattery = GameObject.FindGameObjectsWithTag("Battery");
        save.remainingBatt = savableBattery.Length;

        // Save inventory data
        InventoryManager inventoryManager = PlayerManager.instance.inventoryManager;
        save.playerInventory = new string[inventoryManager.GetMaxInventory()];
        int itemCount = 0;

        List<Item> inventory = inventoryManager.GetInventory();
        foreach (Item item in inventory)
        {
            if (item != null && ItemList.items.ContainsKey(item.itemName)) 
            {
                save.playerInventory[itemCount++] = item.itemName;
            }
        }
        
        // Save all data to data handler
        _dataHandler.Save(save);
    }

    public void DeleteSaveData(int slot)
    {
        ChangeFilename(slot);

        // Save an empty save data
        SaveData save = new SaveData();

        _dataHandler.Save(save);
    }
}
