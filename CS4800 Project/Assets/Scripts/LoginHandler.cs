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
    [SerializeField] TextMeshProUGUI _menuText;
    [SerializeField] GameObject _loginPanel;
    [SerializeField] GameObject _mainMenuPanel;
    private void Start()
    {
        String path = Path.Combine(Application.persistentDataPath, "UserData");
        _dataHandler = new FileDataHandler(path, null);

        // To see the path of files, print:
        Debug.Log(path);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            LoginButton();
        }
    }

    private void OnEnable()
    {
        // TODO: Clear text fields.
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
            MenuMessage("Account not found or password incorrect.", Color.red);
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
            MenuMessage("Invalid username.", Color.red);
        }

        else if(_password == null || _password.Equals("")) {
            MenuMessage("Invalid Password.", Color.red);
        }

        // Checks if user file is in use by a user.
        else if (userData != null && userData.password != null && !userData.password.Equals(""))
        {
            MenuMessage("User already taken.", Color.red);
        }

        // No profile made with this username yet.
        // OR Profile already made but has no password, and is thus available.
        // (Meaning it will override the file — the player statistics in it was generated)
        else
        {
            userData = new UserData(_username, _password);
            _dataHandler.Save(userData);
            MenuMessage("Account created. Please login with your credentials.", Color.green);
        }
    }

    public void RemoveScreen()
    {
        _loginPanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
    }

    // Provides error message within the class with the error text in the login screen.
    private void MenuMessage(string message, Color color)
    {
        _menuText.text = message;
        _menuText.color = color;
    }
}
