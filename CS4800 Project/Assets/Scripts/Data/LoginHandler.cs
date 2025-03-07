using System.IO;
using System;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    private string username;
    private string password;
    FileDataHandler dataHandler;
    private void Start()
    {
        String path = Path.Combine(Application.persistentDataPath, "UserData");
        dataHandler = new FileDataHandler(path, null);

        // To see the path of files, print:
        //Debug.Log(path);
    }

    // End-edit action of username input bar.
    public void SetUsername(string username)
    {
        this.username = username;
        dataHandler.ChangeFilename(username);
    }

    // End-edit action of password input bar.
    public void SetPassword(string password)
    {
        this.password= password;
    }

    // On-press action of Login Button.
    public void LoginButton()
    {
        UserData userData = dataHandler.Load();
        if (userData == null || userData.Password == null)
        {
            // TODO: Add error message for username not found.
        }
        else if(!userData.Password.Equals(password))
        {
            // TODO: Add error message for incorrect password.
        }

        // Password matches with the data.
        else
        {
            // TODO: Add method to allow user in the program.
        }
    }

    // On-press action of Quit Button.
    public void QuitButton()
    {
        Application.Quit();
    }

    // On-press action of Create Account Button
    public void CreateAccountButton()
    {
        // TODO: Implementation of making a new account.
        // Perhaps make a new panel for it + script?
    }
}
