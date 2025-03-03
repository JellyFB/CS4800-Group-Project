using System;

[System.Serializable]
public class GameData  
{

    // Initial testing of presenting the data that we will be pulling for the game
    public string playerName;
    public float averageRunTime;
    public float successRate;

    // Constructor with randomized data for demo
    GameData(string playerName) 
    {
        this.playerName = playerName;

        Random rnd = new Random();
        this.averageRunTime = (float) rnd.NextDouble() * 300 + 120f;
        this.successRate = (float) rnd.NextDouble() * 30 + 70f;
    }

    // Constructor for new player profiles
    GameData(string playerName, float averageRunTime, float successRate)
    {
        this.playerName = playerName;
        this.averageRunTime = averageRunTime;
        this.successRate = successRate;
    }
}
