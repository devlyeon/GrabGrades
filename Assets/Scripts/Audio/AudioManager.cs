using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct AudioSettings
{
    public float main, bgm, sfx;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioPlayer bgmPlayer, sfxPlayer;

    private AudioSettings audioSettings;

    public float MainVolume
    {
        get => audioSettings.main;
        set => audioSettings.main = value;
    }

    public float BgmVolume
    { 
        get => audioSettings.main / 10 * audioSettings.bgm;
        set => audioSettings.bgm = value;
    }

    public float SfxVolume
    { 
        get => audioSettings.main / 10 * audioSettings.sfx;
        set => audioSettings.sfx = value;
    }

    void Awake()
    {
        Data<AudioSettings> data = new("audioPref.dat");
        audioSettings = data.Read();
        bgmPlayer.SetVolume(BgmVolume);
        sfxPlayer.SetVolume(SfxVolume);
    }

    public void OnMainVolumeChanged(float value)
    {
        
    }
}