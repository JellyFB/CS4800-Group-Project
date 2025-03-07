using System;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statisticsText;
    FileDataHandler dataHandler;
    string username;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        String path = Path.Combine(Application.persistentDataPath, "UserData");
        dataHandler = new FileDataHandler(path, null);

        // To see the path of files, print:
        //Debug.Log(path);
    }

    // End-edit action of the input bar
    public void EnterData(string username)
    {
        if (username == null || username.Equals(""))
        {
            Debug.LogError("Username cannot be null or empty!");
        }
        else
        {
            ChangeUsername(username);
            LoadData();
        }
    }

    // Changes username and thus filename.
    public void ChangeUsername(string username)
    {
        this.username = username;
        dataHandler.ChangeFilename($"{username}.json");
    }

    public void LoadData()
    {
        // Creates a dataHandler and loads the gameData from the username
        UserData gameData = dataHandler.Load();
        DisplayText(gameData);
    }

    // On-push action for Generate Button
    public void SaveData()
    {
        if (username == null || username.Equals(""))
        {
            Debug.LogError("Username cannot be null or empty!");
        }
        else
        {
            // Creates a new user profile with randomized gameData for demo
            UserData gameData = new UserData(username);
            dataHandler.Save(gameData);

            LoadData();
        }
        
    }

    private void DisplayText(UserData gameData)
    {
        // Check for existing game data
        if (gameData != null)
        {
            // Casting avg time to int for easier translating to text
            int time = (int)gameData.averageRunTime;

            // Display data on UI
            statisticsText.text = $"<b>Average Run Time</b>: {time / 60:00}:{time % 60:00}\n" +
                $"<b>Success Rate</b>: {gameData.successRate:F1}%\n" +
                $"<b>Total Runs Completed</b>: {gameData.totalRunsCompleted}\n" +
                $"<b>Total Tasks Completed</b>: {gameData.totalTasksCompleted}\n";
        }
        else
        {
            statisticsText.text = $"<b>Average Run Time</b>: N/A\n" +
                $"<b>Success Rate</b>: N/A\n" +
                $"<b>Total Runs Completed</b>: N/A\n" +
                $"<b>Total Tasks Completed</b>: N/A\n";
        }
    }
}
