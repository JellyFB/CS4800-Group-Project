using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour
{

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI runTimeText;
    public TextMeshProUGUI successRateText;

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

        playerNameText = GameObject.Find("Username").GetComponent<TextMeshProUGUI>();
        runTimeText = GameObject.Find("AvgRunTimeStat").GetComponent<TextMeshProUGUI>();
        successRateText = GameObject.Find("SucRateStat").GetComponent<TextMeshProUGUI>();

        //Check for existing game data
        if (gameData != null)
        {
            // Display data on UI
            playerNameText.text = "Username: " + gameData.playerName;
            runTimeText.text = "Average run time: " + gameData.runTime.ToString();
            successRateText.text = "Success rate: " + gameData.successRate.ToString();
        }
        else {
            playerNameText.text = "N/A";
            runTimeText.text = "N/A";
            successRateText.text = "N/A";
        }

        
    }


}
