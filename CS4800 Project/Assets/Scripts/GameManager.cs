using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    // Game-related Data
    [HideInInspector] public string currentUsername;
    [HideInInspector] public string currentPassword;
    private string _sceneName;
    private Stopwatch _stopwatch;

    // Save-related Data
    private SaveData _saveData;
    private int _destroyedTools = 0;
    private int _taskCompletionBit = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        // Get stopwatch component of the game manager
        _stopwatch = GetComponent<Stopwatch>();

        // Assign signals
        SceneManager.sceneLoaded += LoadScene;
    }

    // On-Scene-loaded behavior
    private void LoadScene(Scene scene, LoadSceneMode sceneMode)
    {
        // Reset timer and make sure it is active
        _stopwatch = GetComponent<Stopwatch>();
        _stopwatch.isActive = true;
        _stopwatch.Reset();

        _sceneName = scene.name;
        if (scene.name != "MainMenu")
        {
            // Check if there are any save to load
            if (_saveData != null)
                Invoke("LoadSave", 0.1f);

            // Delaying load tasks so that relevant spawners spawn objects in time
            Invoke("LoadTasks", 0.1f);
        }
    }

    // Assigns the task for each scene
    private void LoadTasks()
    {
        // Clears all instantiated tasks that were loaded from previous levels
        TaskManager.instance.Clear();

        switch (_sceneName)
        {
            case "Level1Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 4);

                // Reading the debris spawned objects
                GameObject[] level1debris = GameObject.FindGameObjectsWithTag("Debris");
        
                TaskManager.instance.AddTask("Remove Debris!", TaskTypes.RemoveDebris, level1debris.Length);

                break;
            case "Level2Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 4);

                // Reading the debris spawned objects
                GameObject[] level2debris = GameObject.FindGameObjectsWithTag("Debris");
        
                TaskManager.instance.AddTask("Remove Debris!", TaskTypes.RemoveDebris, level2debris.Length);

                TaskManager.instance.AddTask("Remove Nails!", TaskTypes.RemoveNails, 4);

                TaskManager.instance.AddTask("Remove Panel!", TaskTypes.RemovePanel, 1);

                // Reading the battery spawned objects
                GameObject[] level2batt = GameObject.FindGameObjectsWithTag("Battery");
        
                TaskManager.instance.AddTask("Remove Battery!", TaskTypes.RemoveBattery, level2batt.Length);
        
                break;
            case "Level3Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 1);

                // Reading the battery spawned objects
                GameObject[] level3batt = GameObject.FindGameObjectsWithTag("Battery");

                TaskManager.instance.AddTask("Dispose of the batteries!", TaskTypes.DisposeBatteries, level3batt.Length);

                break;
            case "Level4Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 2);

                // Reading the battery spawned objects
                GameObject[] level4batt = GameObject.FindGameObjectsWithTag("Battery");

                TaskManager.instance.AddTask("Dispose of the batteries!", TaskTypes.DisposeBatteries, level4batt.Length);

                break;
            case "Level5Scene_Pt1":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 4);

                // Reading the debris spawned objects
                GameObject[] level5pt1debris = GameObject.FindGameObjectsWithTag("Debris");
        
                TaskManager.instance.AddTask("Remove Debris!", TaskTypes.RemoveDebris, level5pt1debris.Length);

                break;
            case "Level5Scene_Pt2":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 4);

                // Reading the debris spawned objects
                GameObject[] level5pt2debris = GameObject.FindGameObjectsWithTag("Debris");
        
                TaskManager.instance.AddTask("Remove Debris!", TaskTypes.RemoveDebris, level5pt2debris.Length);

                TaskManager.instance.AddTask("Remove Nails!", TaskTypes.RemoveNails, 4);

                TaskManager.instance.AddTask("Remove Panel!", TaskTypes.RemovePanel, 1);

                // Reading the battery spawned objects
                GameObject[] level5pt2batt = GameObject.FindGameObjectsWithTag("Battery");
        
                TaskManager.instance.AddTask("Remove Battery!", TaskTypes.RemoveBattery, level5pt2batt.Length);
        
                break;
            case "Level5Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 3);

                //Reading the debris spawned objects
                GameObject[] level5batt = GameObject.FindGameObjectsWithTag("Battery");
        
                TaskManager.instance.AddTask("Dispose of the batteries!", TaskTypes.DisposeBatteries, level5batt.Length);
                break;
            default:
                Debug.Log("Scene not recognized");
                break;
        }

        // Save related task behavior
        // If some tools were loaded in, the corresponding task is incremented
        TaskManager.instance.SetTaskProgress(TaskTypes.GetTools, _destroyedTools);
        _destroyedTools = 0;

        // Completes task depending on the save data
        foreach (TaskTypes type in Enum.GetValues(typeof(TaskTypes)))
        {
            if ((_taskCompletionBit & (int) type) > 0)
            {
                TaskManager.instance.ForceFinishTask(type);
            }
        }

        _taskCompletionBit = 0;
    }

    // Will save the statistics
    public void CompleteLevel()
    {
        // Stop timer
        _stopwatch.isActive = false;

        TaskManager.instance.TaskCount();
    }

    // Loads the save between scenes
    public void OnLoadSave(SaveData save)
    {
        _saveData = save;
        SceneManager.LoadScene(_saveData.levelNumber);
    }

    // Loads save if it exists
    private void LoadSave()
    {
        // Load the save data
        PlayerManager.instance.player.transform.position = _saveData.playerPosition;
        _stopwatch.SetTime(_saveData.gameTime);
        _taskCompletionBit = _saveData.taskCompletionBit;

        // Load inventory
        foreach (string itemName in _saveData.playerInventory)
        {
            if (!itemName.Equals(""))
            {
                Item item = ItemList.GetItem(itemName);

                // Give player the item
                PlayerManager.instance.inventoryManager.PickupItem(item);

                // NOTE: This next few sections should only be for tools but since
                // we're only saving tools, there's no checks for it.
                // Destroy any objects with the name of the tool
                GameObject target = GameObject.Find(itemName);
                if (target != null)
                {
                    Destroy(target);
                    _destroyedTools++;
                }
            }
        }
        
        // Remove the save from game manager
        _saveData = null;
    }

    // STOPWATCH METHODS

    // Pauses / unpauses stopwatch
    public void PauseGameTime(bool isPaused)
    {
        _stopwatch.isActive = !isPaused;
    }

    // Returns the stopwatch time
    public float GetLevelTime()
    {
        return _stopwatch.GetTime();
    }
}
