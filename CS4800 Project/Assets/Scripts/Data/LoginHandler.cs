using System.IO;
using System;
using UnityEngine;
using System.Buffers;
using TMPro;

public class LoginHandler : MonoBehaviour
{
    private string username;
    private string password;
    FileDataHandler dataHandler;

    [Header("Login UI")]
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject mainMenuPanel;
    private void Start()
    {
        String path = Path.Combine(Application.persistentDataPath, "UserData");
        dataHandler = new FileDataHandler(path, null);

        // To see the path of files, print:
        Debug.Log(path);
    }

    // End-edit action of username input bar.
    public void SetUsername(string username)
    {
        this.username = username;
        dataHandler.ChangeFilename($"{username}.json");
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
        if (userData == null || userData.password == null
            || !userData.password.Equals(password))
        {
            errorText.text = "Account not found or password incorrect.";
        }

        // Password matches with the data.
        else
        {
            RemoveScreen();
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
        // TODO: Make new panel for new creating an account,
        // perhaps new script too?
        UserData userData = dataHandler.Load();
        if (username == null || username.Equals(""))
        {
            errorText.text = "Invalid username.";
        }
        else if (userData != null)
        {
            if (userData.password != null)
            {
                errorText.text = "User already taken.";
            }
            
            // Profile already made but has no password, and is thus
            // available.
            else
            {
                userData.password = password;
                dataHandler.Save(userData);
                RemoveScreen();
            }
        }

        // No profile made with this username yet.
        else
        {
            userData = new UserData(username, password);
            dataHandler.Save(userData);
            RemoveScreen();
        }
    }

    public void RemoveScreen()
    {
        loginPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
