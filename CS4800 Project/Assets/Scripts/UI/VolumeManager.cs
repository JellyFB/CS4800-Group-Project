using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;
    [SerializeField] private AudioMixer audioMixer;

    public const string MASTER_KEY = "MasterVolume";
    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SFXVolume";

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Load volume settings
    void Load()
    {
        float MasterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1000f);
        float MusicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1000f);
        float SFXVolume = PlayerPrefs.GetFloat(SFX_KEY, 1000f);

        if (MasterVolume > 1000) MasterVolume += (MasterVolume - 1000) * 8;
        if (MusicVolume > 1000) MusicVolume += (MusicVolume - 1000) * 8;
        if (SFXVolume > 1000) SFXVolume += (SFXVolume - 1000) * 8;

        audioMixer.SetFloat(SettingsMenuManager.MASTER_KEY,Mathf.Log10(MasterVolume/1000) * 20);
        audioMixer.SetFloat(SettingsMenuManager.MUSIC_KEY,Mathf.Log10(MusicVolume/1000) * 20);
        audioMixer.SetFloat(SettingsMenuManager.SFX_KEY,Mathf.Log10(SFXVolume/1000) * 20);
    }
}
