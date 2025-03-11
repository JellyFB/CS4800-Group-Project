using System.IO;
using System;
using UnityEngine;
using System.Buffers;
using TMPro;

public class LoginHandler : MonoBehaviour
{
    private string _username;
    private string _password;
    FileDataHandler _dataHandler;

    [Header("Login UI")]
    [SerializeField] TextMeshProUGUI _errorText;
    [SerializeField] GameObject _loginPanel;
    [SerializeField] GameObject _mainMenuPanel;
    private void Start()
    {
        String path = Path.Combine(Application.persistentDataPath, "UserData");
        _dataHandler = new FileDataHandler(path, null);

        // To see the path of files, print:
        Debug.Log(path);
    }

    // End-edit action of username input bar.
    public void SetUsername(string username)
    {
        this._username = username;
        _dataHandler.ChangeFilename($"{username}.json");
    }

    // End-edit action of password input bar.
    public void SetPassword(string password)
    {
        this._password= password;
    }

    // On-press action of Login Button.
    public void LoginButton()
    {
        UserData userData = _dataHandler.Load();
        if (userData == null || userData.password == null
            || !userData.password.Equals(_password))
        {
            ErrorMessage("Account not found or password incorrect.", "red");
        }

        // Password matches with the data.
        else
        {
            CurrentUser.s_username = _username;
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
        UserData userData = _dataHandler.Load();
        if (_username == null || _username.Equals(""))
        {
            ErrorMessage("Invalid username.", "red");
        }
        else if (userData != null)
        {
            if (userData.password != null)
            {
                ErrorMessage("User already taken.", "red");
            }
            
            // Profile already made but has no password, and is thus
            // available.
            else
            {
                userData.password = _password;
                _dataHandler.Save(userData);
                RemoveScreen();
            }
        }

        // No profile made with this username yet.
        else
        {
            userData = new UserData(_username, _password);
            _dataHandler.Save(userData);
            ErrorMessage("Account created. Please login with your credentials.", "green");
        }
    }

    public void RemoveScreen()
    {
        _loginPanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
    }

    // Provides error message within the class with the error text in the login screen.
    private void ErrorMessage(string message, string color)
    {
        _errorText.text = message;
        switch (color)
        {
            case "red":
                _errorText.color = new Color(1.0f, 0f, 0f);
                break;
            case "blue":
                _errorText.color = new Color(0f, 0f, 1.0f);
                break;
            case "green":
                _errorText.color = new Color(0f, 1.0f, 0f);
                break;
            default:
                Debug.LogError("Invalid color used for text.");
                break;
        }
    }
}
