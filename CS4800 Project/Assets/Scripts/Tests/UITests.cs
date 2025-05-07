using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsTests
{
    private GameObject settingsMenuObject;
    private SettingsMenuManager settingsMenuManager;

    [SetUp]
    public void SetUp()
    {
        // Create GameObject with the SettingsMenuManager component
        settingsMenuObject = new GameObject("SettingsMenu");
        settingsMenuManager = settingsMenuObject.AddComponent<SettingsMenuManager>();

        // Create and assign sliders
        var masterSliderObj = new GameObject("MasterSlider");
        var musicSliderObj = new GameObject("MusicSlider");
        var sfxSliderObj = new GameObject("SFXSlider");

        var masterSlider = masterSliderObj.AddComponent<Slider>();
        var musicSlider = musicSliderObj.AddComponent<Slider>();
        var sfxSlider = sfxSliderObj.AddComponent<Slider>();

        settingsMenuManager.MasterSlider = masterSlider;
        settingsMenuManager.MusicSlider = musicSlider;
        settingsMenuManager.SFXSlider = sfxSlider;

        // Create and assign TMP_Dropdown
        var dropdownObj = new GameObject("GraphicsDropdown");
        var dropdown = dropdownObj.AddComponent<TMP_Dropdown>();

        // Add a few dummy options to simulate actual quality settings
        dropdown.options.Add(new TMP_Dropdown.OptionData("Low"));
        dropdown.options.Add(new TMP_Dropdown.OptionData("Medium"));
        dropdown.options.Add(new TMP_Dropdown.OptionData("High"));
        dropdown.value = 2; // e.g., "High"

        settingsMenuManager.graphicsDropdown = dropdown;

        // We skip audioMixer assignment for now — update your SetVolume to handle nulls

        // Manually invoke Unity lifecycle methods
        settingsMenuManager.Awake();
        settingsMenuManager.Start();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(settingsMenuObject);
    }

    [Test]
    public void Test_Graphics_Change()
    {
        // Act
        settingsMenuManager.ChangeGraphicsQuality();

        // Assert (Check if QualitySettings level is updated)
        Assert.AreEqual(settingsMenuManager.graphicsDropdown.value, QualitySettings.GetQualityLevel());
    }
}
