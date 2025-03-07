using System;

[System.Serializable]
public class UserData  
{

    // Initial testing of presenting the data that we will be pulling for the game
    public string playerName;
    private string password;
    public float averageRunTime;
    public float successRate;
    public int totalRunsCompleted;
    public int totalTasksCompleted;
    
    // Getter / setter for password in case we want to change type of password later.
    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    // Constructor with randomized data for demo
    public UserData(string playerName) 
    {
        this.playerName = playerName;
        this.password = null;

        Random rnd = new Random();
        this.averageRunTime = (float) rnd.NextDouble() * 300 + 120f;
        this.successRate = (float) rnd.NextDouble() * 30 + 70f;
        this.totalRunsCompleted = rnd.Next(0, 50);
        this.totalTasksCompleted = this.totalRunsCompleted * 5 + 
            rnd.Next(0, this.totalRunsCompleted * 2);
    }

    // Constructor for new player profiles
    public UserData(string playerName, string password)
    {
        this.playerName = playerName;
        this.password = password;

        averageRunTime = 0;
        successRate = 0;
        totalRunsCompleted = 0;
        totalTasksCompleted = 0;
    }
}
