using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    // Data
    [HideInInspector] public string currentUsername;
    private string _sceneName;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += LoadScene;
    }

    // On-Scene-loaded behavior
    private void LoadScene(Scene scene, LoadSceneMode sceneMode)
    {
        _sceneName = scene.name;
        if (scene.name != "Main Menu")
        {
            // Delaying load tasks so that relevant spawners spawn objects in time
            Invoke("LoadTasks", 0.1f);
        }
    }

    // Assigns the task for each scene
    private void LoadTasks()
    {
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
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 1);

                // Reading the battery spawned objects
                GameObject[] level4batt = GameObject.FindGameObjectsWithTag("Battery");

                TaskManager.instance.AddTask("Dispose of the batteries!", TaskTypes.DisposeBatteries, level4batt.Length);

                break;
            case "Level5Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 4);

                //Reading the debris spawned objects
                GameObject[] level5debris = GameObject.FindGameObjectsWithTag("Battery");
        
                TaskManager.instance.AddTask("Dispose of the batteries!", TaskTypes.DisposeBatteries, level5debris.Length);

                break;
            default:
                Debug.Log("Scene not recognized");
                break;
        }
    }
}
