using System;

[System.Serializable]
public class GameData  
{

    // Initial testing of presenting the data that we will be pulling for the game
    public string playerName;
    public float averageRunTime;
    public float successRate;
    public int totalRunsCompleted;
    public int totalTasksCompleted;

    // Constructor with randomized data for demo
    public GameData(string playerName) 
    {
        this.playerName = playerName;

        Random rnd = new Random();
        this.averageRunTime = (float) rnd.NextDouble() * 300 + 120f;
        this.successRate = (float) rnd.NextDouble() * 30 + 70f;
        this.totalRunsCompleted = rnd.Next(0, 50);
        this.totalTasksCompleted = this.totalRunsCompleted * 5 + 
            rnd.Next(0, this.totalRunsCompleted * 2);
    }

    // Constructor for new player profiles
    public GameData(string playerName, float averageRunTime, float successRate,
        int totalRunsCompleted, int totalTasksCompleted)
    {
        this.playerName = playerName;
        this.averageRunTime = averageRunTime;
        this.successRate = successRate;
        this.totalRunsCompleted = totalRunsCompleted;
        this.totalTasksCompleted = totalTasksCompleted;
    }
}
