using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutuionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider VolumeSlider;

    Resolution[] resolutions;

    private void LoadPref()
    {
        try
        {
            if (PlayerPrefs.GetInt("Fullscreen") == 1)
            {
                SetFullscreen(true);
            }
            else
            {
                SetFullscreen(false);
            }
            SetVolume(PlayerPrefs.GetFloat("Volume"));
            SetQuality(PlayerPrefs.GetInt("Quality"));
            SetResolution(PlayerPrefs.GetInt("Resolution"));
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
            VolumeSlider.value = PlayerPrefs.GetFloat("Volume");
            resolutuionDropdown.value = PlayerPrefs.GetInt("Resolution");
            resolutuionDropdown.RefreshShownValue();
            qualityDropdown.RefreshShownValue();
        }
        catch { }
    }

    private void Start()
    {      
            resolutions = Screen.resolutions;
            resolutuionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + " Hz";
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutuionDropdown.AddOptions(options);
            resolutuionDropdown.value = currentResolutionIndex;
            resolutuionDropdown.RefreshShownValue();      
        LoadPref();    
    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
        PlayerPrefs.Save();
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (Screen.fullScreen)
        {
            PlayerPrefs.SetInt("Fullscreen",1);
        PlayerPrefs.Save();
        }
        else 
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
            PlayerPrefs.Save();
        }
        
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
        PlayerPrefs.Save();
    }
}
