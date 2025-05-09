using System.IO;
using System;
using UnityEngine;
using Unity.VisualScripting;

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

        save.username = GameManager.instance.currentUsername;
        save.levelNumber = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        save.numberOfTasks = TaskManager.instance.TaskCount();
        save.completedTasks = TaskManager.instance.FinishedTaskCount();
        save.gameTime = GameManager.instance.GetLevelTime();
        save.playerPosition = PlayerManager.instance.player.transform.position;
        GameObject[] savableDebris = GameObject.FindGameObjectsWithTag("Debris");
        save.remainingDebris = savableDebris.Length;
        GameObject[] savableBattery = GameObject.FindGameObjectsWithTag("Battery");
        save.remainingBatt = savableBattery.Length;
        
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
