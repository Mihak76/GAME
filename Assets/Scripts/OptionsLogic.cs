using UnityEngine;
using UnityEngine.UI;

public class OptionsLogic : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider mouseSensitivitySlider;

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}