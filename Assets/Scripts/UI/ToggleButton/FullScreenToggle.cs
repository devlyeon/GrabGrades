using System;
using UnityEngine;

[Serializable]
public struct FullScreenSetting { public bool isFullScreen; }

public class FullScreenToggle : ToggleButton
{
    private Data<FullScreenSetting> data;
    private ResolutionSetting resolution;

    protected override void Awake()
    {
        data = new("displayPref_01.dat");
        Data<ResolutionSetting> resolutionData = new("displayPref_01.dat");
        resolution = resolutionData.Read();

        isActive = Screen.fullScreen;
        base.Awake();
    }

    public override void Toggle()
    {
        if (isActive)
            Screen.fullScreenMode = FullScreenMode.Windowed;
        else
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.SetResolution(resolution.width, resolution.height, !isActive);
        data.Write(new FullScreenSetting(){ isFullScreen = !isActive });
        base.Toggle();
    }
}