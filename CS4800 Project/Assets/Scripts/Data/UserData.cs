using System;

[System.Serializable]
public class UserData : Data
{

    // Initial testing of presenting the data that we will be pulling for the game
    public string playerName;
    public string password;
    public float averageRunTime;
    public int totalRunsCompleted;
    public int totalTasksCompleted;

    // Constructor for new player profiles
    public UserData(string playerName, string password)
    {
        this.playerName = playerName;
        this.password = password;

        averageRunTime = 0;
        totalRunsCompleted = 0;
        totalTasksCompleted = 0;
    }
}
