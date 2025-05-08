using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    [SerializeField] public AudioMixer audioMixer;

    [SerializeField] public Slider MasterSlider, MusicSlider, SFXSlider;

    public const string MASTER_KEY = "Master";
    public const string MUSIC_KEY = "Music";
    public const string SFX_KEY = "SFX";

    public virtual void Awake()
    {
        if (MasterSlider != null)
            MasterSlider.onValueChanged.AddListener((value) => { SetVolume(MASTER_KEY, value); });
        else
            Debug.LogWarning("MasterSlider is not assigned!");

        if (MusicSlider != null)
            MusicSlider.onValueChanged.AddListener((value) => { SetVolume(MUSIC_KEY, value); });
        else
            Debug.LogWarning("MusicSlider is not assigned!");

        if (SFXSlider != null)
            SFXSlider.onValueChanged.AddListener((value) => { SetVolume(SFX_KEY, value); });
        else
            Debug.LogWarning("SFXSlider is not assigned!");
    }

    public virtual void Start()
    {
        if (MasterSlider != null)
            MasterSlider.value = PlayerPrefs.GetFloat(MASTER_KEY, 1000f);

        if (MusicSlider != null)
            MusicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, 1000f);

        if (SFXSlider != null)
            SFXSlider.value = PlayerPrefs.GetFloat(SFX_KEY, 1000f);
    }

    public virtual void OnDisable()
    {
        if (MasterSlider != null)
            PlayerPrefs.SetFloat(MASTER_KEY, MasterSlider.value);

        if (MusicSlider != null)
            PlayerPrefs.SetFloat(MUSIC_KEY, MusicSlider.value);

        if (SFXSlider != null)
            PlayerPrefs.SetFloat(SFX_KEY, SFXSlider.value);
    }

    public void ChangeGraphicsQuality()
    {
        if (graphicsDropdown != null)
        {
            QualitySettings.SetQualityLevel(graphicsDropdown.value);
            Debug.Log("Quality Settings Updated.");
        }
        else
        {
            Debug.LogWarning("Graphics Dropdown not assigned!");
        }
    }

    // Set volume based on value of slider from 1 - 2000
    public void SetVolume(string audio, float value)
    {
        if (audioMixer == null) return;

        if (value > 1000)
            value += (value - 1000) * 8; // Adjusted increase from midpoint (default) volume

        float volume = Mathf.Log10(value / 1000) * 20; // Volume is scaled logarithmically
        audioMixer.SetFloat(audio, volume);
    }
}
