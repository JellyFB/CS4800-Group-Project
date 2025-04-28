using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    // Data
    [HideInInspector] public string currentUsername;

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
        Debug.Log("1");

        if (scene.name != "Main Menu")
        {
            LoadTasks(scene.name);
        }
    }

    // Assigns the task for each scene
    private void LoadTasks(String sceneName)
    {
        Debug.Log("2");
        switch (sceneName)
        {
            case "Level1Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 4);
                break;
            case "Level2Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 4);
                break;
            case "Level3Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 2);
                break;
            case "Level4Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 2);
                break;
            case "Level5Scene":
                TaskManager.instance.AddTask("Get tools!", TaskTypes.GetTools, 2);
                break;
            default:
                Debug.Log("Scene not recognized");
                break;
        }
    }
}
