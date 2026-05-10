using UnityEngine;

public class Splash : MonoBehaviour
{
    void Awake()
    {
        Data<FullScreenSetting> fullScreen = new("displayPref_01.dat");
        Data<ResolutionSetting> resolution = new("displayPref_02.dat");
        bool isFullScreen;
        ResolutionSetting resolutionValue;
        if (fullScreen.Exists()) isFullScreen = fullScreen.Read().isFullScreen;
        else
        {
            isFullScreen = true;
            fullScreen.Write(new FullScreenSetting(){isFullScreen = true});
        }
        if (resolution.Exists()) resolutionValue = resolution.Read();
        else
        {
            resolutionValue = new ResolutionSetting(){width=1920, height=1080};
            resolution.Write(resolutionValue);
        }
        Screen.SetResolution(resolutionValue.width, resolutionValue.height, isFullScreen);
        
    }
}