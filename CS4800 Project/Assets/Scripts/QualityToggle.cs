using UnityEngine;
using UnityEngine.UI;

public class QualityToggle : MonoBehaviour
{
    public int qualityLevel; // Set this in the Inspector
    private Toggle toggle;

    public void SetQuality()
    {
        if (toggle.isOn)
        {
            QualitySettings.SetQualityLevel(qualityLevel);
        }
    }

}
