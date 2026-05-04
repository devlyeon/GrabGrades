using UnityEngine;

public class Splash : MonoBehaviour
{
    void Awake()
    {
        Data<FullScreenSetting> fullScreen = new("displayPref_01.dat");
        Data<ResolutionSetting> resolution = new("displayPref_02.dat");
        bool isFullScreen = fullScreen.Read().isFullScreen;
        ResolutionSetting resolutionValue = resolution.Read();
        Screen.SetResolution(resolutionValue.width, resolutionValue.height, isFullScreen);
    }
}