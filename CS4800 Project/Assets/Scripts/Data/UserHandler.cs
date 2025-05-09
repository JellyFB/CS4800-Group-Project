using System.IO;
using System;
using UnityEngine;

public class UserHandler
{
    FileDataHandler<UserData> _dataHandler;

    public UserHandler()
    {
        String path = Path.Combine(Application.persistentDataPath, "UserData");
        _dataHandler = new FileDataHandler<UserData>(path, GameManager.instance.currentUsername);
    }

    // Retrieves data from the specific user
    public UserData GetUserData()
    {
            UserData user = _dataHandler.Load();
            return user;
    }

    // Writing to a new user that will override the current user
    public void WriteUserData(UserData user)
    {
        _dataHandler.Save(user);
    }

}
