using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider MasterSlider, MusicSlider, SFXSlider;

    void Awake ()
    {
        MasterSlider.onValueChanged.AddListener((value) => {SetVolume("Master", value);});
        MusicSlider.onValueChanged.AddListener((value) => {SetVolume("Music", value);});
        SFXSlider.onValueChanged.AddListener((value) => {SetVolume("SFX", value);});
    }

    public void ChangeGraphicsQuality()
    {
	    QualitySettings.SetQualityLevel(graphicsDropdown.value);
        Debug.Log("Quality Settings Updated.");
    }

    // Set volume based on value of slider
    public void SetVolume(string audio,float value)
    {
        if (value > 1000) value += (value - 1000) * 8; //Adjusted increase value from midpoint(default) volume
        float volume = Mathf.Log10(value/1000) * 20; //Volume is scaled logrithmically
        audioMixer.SetFloat(audio,volume);
    }
}
