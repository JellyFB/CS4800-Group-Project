using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting;

/*
 * Copied mostly through the video : https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-tMGkSApPdu4hlUBagKial&index=4
 */
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    // Handles loading game data from a JSON file
    public GameData Load()
    {
        // Using Path.Combine to account for different OS's having diff path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // Load the serialized data from the file
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // Deserialize the data from the file
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occurred while loading in file: {fullPath}\n{e}");
            }

            
        }

        return loadedData;
    }

    // Handles saving to JSON file
    public void Save(GameData data)
    {
        // Using Path.Combine to account for different OS's having diff path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // Create the directory the file will be written into if it doesn't already exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize the C# game data object to JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e) 
        {
            Debug.LogError($"Error occurred when trying to save data to file: {fullPath}\n{e}");
        }
    }

    public void ChangeFilename(string name)
    {
        dataFileName = name;
    }

}
