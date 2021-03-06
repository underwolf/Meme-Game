using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer m_AudioMixer;
    public Dropdown m_ResolutionDropdown;
    public Dropdown quality;
    private Resolution[] m_Resolutions;

    private void Start()
    {
        m_Resolutions = Screen.resolutions;
        m_ResolutionDropdown.ClearOptions();

        var options = new List<string>();
        foreach(var resolution in m_Resolutions)
        {
            var option = $"{resolution.width} x {resolution.height}";
            options.Add(option);
        }
        var currentResolution = $"{Screen.currentResolution.width} x {Screen.currentResolution.height}";
        m_ResolutionDropdown.AddOptions(options);
        m_ResolutionDropdown.value = options.FindIndex(resolution => resolution.Equals(currentResolution));
        m_ResolutionDropdown.RefreshShownValue();
        quality.value = QualitySettings.GetQualityLevel();
    }

    public void SetResolution(int resolutionIndex)
    {
        var resolution = m_Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        m_AudioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
