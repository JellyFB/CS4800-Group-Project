using UnityEngine;

public class LoadJSON : MonoBehaviour
{
    public UserData LoadPlayerData()
    {
        // Load the JSON file from Resources
        TextAsset jsonFile = Resources.Load<TextAsset>("data");
        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found!");
            return null;
        }

        // Deserialize JSON into PlayerData object
        UserData gameData = JsonUtility.FromJson<UserData>(jsonFile.text);
        return gameData;
    }
}
