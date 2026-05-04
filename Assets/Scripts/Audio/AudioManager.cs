using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct AudioSettings
{
    public float main, bgm, sfx;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [SerializeField] private AudioPlayer bgmPlayer, sfxPlayer;

    private AudioSettings audioSettings;
    private Data<AudioSettings> data;

    public float MainVolume
    {
        get => audioSettings.main;
        set
        {
            audioSettings.main = value;
            bgmPlayer.Volume = BgmVolume;
            sfxPlayer.Volume = SfxVolume;
            data.Write(audioSettings);
        }
    }

    public float BgmVolume
    { 
        get => audioSettings.main * audioSettings.bgm;
        set
        {
            audioSettings.bgm = value;
            bgmPlayer.Volume = BgmVolume;
            data.Write(audioSettings);
        }
    }

    public float SfxVolume
    { 
        get => audioSettings.main * audioSettings.sfx;
        set
        {
            audioSettings.sfx = value;
            sfxPlayer.Volume = SfxVolume;
            data.Write(audioSettings);
        }
    }

    void Awake()
    {
        if (audioManager != null) Destroy(gameObject);
        else
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
            bgmPlayer.Play(0);
        }

        data = new("audioPref.dat");
        if (data.Exists()) audioSettings = data.Read();
        else
        {
            audioSettings.main = 1.0f;
            audioSettings.bgm = 1.0f;
            audioSettings.sfx = 1.0f;
        }

        bgmPlayer.Volume = BgmVolume;
        sfxPlayer.Volume = SfxVolume;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            sfxPlayer.Play(0);
    }

    public void PlaySfx(int id)
    {
        sfxPlayer.Play(id);
    }
}