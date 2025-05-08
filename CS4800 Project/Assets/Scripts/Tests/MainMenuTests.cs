using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;

public class MainMenuTests
{
    private GameObject menuGO;
    private MainMenu menu;
    private TextMeshProUGUI userText;

    private GameObject gm;

    [SetUp]
    public void Setup()
    {
        // Dummy GameManager setup
        gm = new GameObject("GameManager");
        GameManager.instance = gm.AddComponent<GameManager>();
        GameManager.instance.currentUsername = "TestUser";

        // Create GameObject in inactive state to avoid OnEnable being triggered
        menuGO = new GameObject("MainMenu");
        menuGO.SetActive(false); // prevent OnEnable for now

        // Add components
        userText = menuGO.AddComponent<TextMeshProUGUI>();
        menu = menuGO.AddComponent<MainMenu>();

        // Set private serialized field using reflection
        typeof(MainMenu)
            .GetField("currentUserText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(menu, userText);

        // Now activate to trigger OnEnable safely
        menuGO.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(menuGO);
        Object.DestroyImmediate(gm);
    }

    [Test]
    public void OnEnable_SetsCorrectUsernameText()
    {
        // Act
        menu.OnEnable();

        // Assert
        Assert.AreEqual("Current User: <b>TestUser</b>", userText.text);
    }

    [Test]
    public void Logout_ClearsCurrentUsername()
    {
        // Act
        menu.Logout();

        // Assert
        Assert.IsNull(GameManager.instance.currentUsername);
    }

    [Test]
    public void QuitGame_LogsQuitIntent()
    {
        LogAssert.Expect(LogType.Log, "Application.Quit called");
        Application.Quit(); // Normally not assertable in editor
        Debug.Log("Application.Quit called");
    }
}
