using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;

    GameManager manager;

    Resolution[] resolutions;
    void Start()
    {
        manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
       

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> reolutionsList = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            reolutionsList.Add(option);
            
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }    
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(reolutionsList);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        volumeSlider.value = manager.soundVolume;
    }


    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume",volume);

    }

    public void SetFullscreen(bool fullscreenMode)
    {
        Screen.fullScreen = fullscreenMode;
    }

    public void setResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);

    }

    public void saveOptions()
    {

        float value;
        audioMixer.GetFloat("volume", out value);
        manager.soundVolume = value;
        manager.screenWidth = Screen.currentResolution.width;
        manager.screenHeight = Screen.currentResolution.height;
        manager.fullScreen = Screen.fullScreen;

        manager.SaveData();



    }

}
