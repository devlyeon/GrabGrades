using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[Serializable]
public struct ResolutionSetting { public int width, height; }


public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private Data<ResolutionSetting> data;
    private bool isFullScreen = true;
    private readonly List<Resolution> resolutions = new();

    void Awake()
    {
        Data<FullScreenSetting> fullScreen = new("displayPref_01.dat");
        isFullScreen = fullScreen.Read().isFullScreen;
        data = new("displayPref_02.dat");
        dropdown.options.Clear();
        
        List<string> resolutionText = new();
        foreach (Resolution resolution in Screen.resolutions)
        {
            resolutions.Add(resolution);
            resolutionText.Add(resolution.width + " X " + resolution.height);
        }
        dropdown.AddOptions(resolutionText);

        ResolutionSetting current = data.Read();
        string currentRes = current.width + " X " + current.height;
        if (resolutionText.Contains(currentRes))
            dropdown.value = resolutionText.IndexOf(currentRes);
    }

    public void SetResolution(int id)
    {
        Screen.fullScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(resolutions[id].width, resolutions[id].height, isFullScreen);
        data.Write(new ResolutionSetting(){ width = resolutions[id].width, height = resolutions[id].height });
    }
}