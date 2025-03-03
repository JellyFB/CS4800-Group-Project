using UnityEngine;

public class LoadJSON : MonoBehaviour
{
    public GameData LoadPlayerData()
    {
        // Load the JSON file from Resources
        TextAsset jsonFile = Resources.Load<TextAsset>("data");
        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found!");
            return null;
        }

        // Deserialize JSON into PlayerData object
        GameData gameData = JsonUtility.FromJson<GameData>(jsonFile.text);
        return gameData;
    }
}
