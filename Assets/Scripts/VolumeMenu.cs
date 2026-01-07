using UnityEngine;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Naloži shranjen volume (default = 1)
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        AudioListener.volume = volume;
        volumeSlider.value = volume;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
