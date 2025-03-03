using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI statisticsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Load the JSON file from Resources
        TextAsset jsonFile = Resources.Load<TextAsset>("data");
        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found!");
        }

        // Deserialize JSON into PlayerData object
        GameData gameData = JsonUtility.FromJson<GameData>(jsonFile.text);

        // Check for existing game data
        if (gameData != null)
        {
            // Casting avg time to int for easier translating to text
            int time = (int)gameData.averageRunTime;

            // Display data on UI
            playerNameText.text = $"<b>Username</b>: {gameData.playerName}";
            statisticsText.text = $"<b>Average run time</b>: {time / 60:00}:{time % 60:00}\n" + 
                $"<b>Success rate</b>: {gameData.successRate}%\n";
        }
        else
        {
            playerNameText.text = "<b>Username</b>: N/A";
            statisticsText.text = $"<b>Average run time</b>: N/A\n" +
                $"<b>Success rate</b>: N/A\n";
        }
    }
}
