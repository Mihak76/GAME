using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;
    List<Resolution> uniqueResolutions = new List<Resolution>();

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        foreach (Resolution res in resolutions)
        {
            // Preveri, če ta resolucija že obstaja
            bool exists = false;
            foreach (Resolution unique in uniqueResolutions)
            {
                if (unique.width == res.width && unique.height == res.height)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                uniqueResolutions.Add(res);
                options.Add(res.width + " x " + res.height);

                if (res.width == Screen.currentResolution.width &&
                    res.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = uniqueResolutions.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution res = uniqueResolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
