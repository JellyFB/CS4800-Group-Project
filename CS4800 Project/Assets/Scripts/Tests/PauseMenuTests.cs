using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuTests
{
    private GameObject pauseMenuObject;
    private PauseMenu pauseMenu;
    
    private GameObject pauseUI;
    private GameObject settingsUI;
    private GameObject bindsUI;

    [SetUp]
    public void SetUp()
    {
        pauseMenuObject = new GameObject("PauseMenu");
        pauseMenu = pauseMenuObject.AddComponent<PauseMenu>();

        // Create UI elements and assign them
        pauseUI = new GameObject("PauseUI");
        settingsUI = new GameObject("SettingsUI");
        bindsUI = new GameObject("BindsUI");

        pauseMenuObject.SetActive(true); // Ensure the object is enabled before Start() is called

        // Use reflection or serialized fields assignment if needed
        var pauseField = typeof(PauseMenu).GetField("_pauseMenu", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var settingsField = typeof(PauseMenu).GetField("_settingsMenu", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var bindsField = typeof(PauseMenu).GetField("_bindsMenu", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        pauseField.SetValue(pauseMenu, pauseUI);
        settingsField.SetValue(pauseMenu, settingsUI);
        bindsField.SetValue(pauseMenu, bindsUI);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(pauseMenuObject);
        Object.DestroyImmediate(pauseUI);
        Object.DestroyImmediate(settingsUI);
        Object.DestroyImmediate(bindsUI);
    }

    [Test]
    public void PauseGame_ActivatesPauseUI()
    {
        // Act
        pauseMenu.PauseGame();

        // Assert
        Assert.IsTrue(pauseUI.activeSelf);
        Assert.IsTrue(pauseMenuObject.activeSelf);
        Assert.AreEqual(0f, Time.timeScale);
    }

    [Test]
    public void ResumeGame_DeactivatesAllUI()
    {
        pauseMenu.PauseGame(); // Pause first

        // Act
        pauseMenu.ResumeGame();

        // Assert
        Assert.IsFalse(pauseUI.activeSelf);
        Assert.IsFalse(settingsUI.activeSelf);
        Assert.IsFalse(bindsUI.activeSelf);
        Assert.AreEqual(1f, Time.timeScale);
    }
}
