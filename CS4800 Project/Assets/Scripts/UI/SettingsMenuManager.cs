using TMPro;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;

    public void ChangeGraphicsQuality()
    {
	    QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
}
