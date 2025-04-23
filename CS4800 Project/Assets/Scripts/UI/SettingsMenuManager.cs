using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider MasterSlider, MusicSlider, SFXSlider;
    public const string MASTER_KEY = "Master";
    public const string MUSIC_KEY = "Music";
    public const string SFX_KEY = "SFX";

    void Awake ()
    {
        MasterSlider.onValueChanged.AddListener((value) => {SetVolume(MASTER_KEY, value);});
        MusicSlider.onValueChanged.AddListener((value) => {SetVolume(MUSIC_KEY, value);});
        SFXSlider.onValueChanged.AddListener((value) => {SetVolume(SFX_KEY, value);});
    }

    void Start ()
    {
        MasterSlider.value = PlayerPrefs.GetFloat(VolumeManager.MASTER_KEY, 1000f);
        MusicSlider.value = PlayerPrefs.GetFloat(VolumeManager.MUSIC_KEY, 1000f);
        SFXSlider.value = PlayerPrefs.GetFloat(VolumeManager.SFX_KEY, 1000f);
    }

    void OnDisable ()
    {
        PlayerPrefs.SetFloat(VolumeManager.MASTER_KEY, MasterSlider.value);
        PlayerPrefs.SetFloat(VolumeManager.MUSIC_KEY, MusicSlider.value);
        PlayerPrefs.SetFloat(VolumeManager.SFX_KEY, SFXSlider.value);
    }

    public void ChangeGraphicsQuality()
    {
	    QualitySettings.SetQualityLevel(graphicsDropdown.value);
        Debug.Log("Quality Settings Updated.");
    }

    // Set volume based on value of slider from 1 - 2000
    public void SetVolume(string audio,float value)
    {
        if (value > 1000) value += (value - 1000) * 8; //Adjusted increase value from midpoint(default) volume
        float volume = Mathf.Log10(value/1000) * 20; //Volume is scaled logrithmically
        audioMixer.SetFloat(audio,volume);
    }
}
