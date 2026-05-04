using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum VolumeType { NONE, MAIN, BGM, SFX };

public class SliderDisplay : MonoBehaviour
{
    [SerializeField] private VolumeType type;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private Slider slider;

    void Awake()
    {
        Data<AudioSettings> data = new("audioPref.dat");
        switch (type)
        {
            case VolumeType.MAIN:
                SetValue(data.Read().main);
                break;
            case VolumeType.BGM:
                SetValue(data.Read().bgm);
                break;
            case VolumeType.SFX:
                SetValue(data.Read().sfx);
                break;
            default:
                break;
        }
    }

    public void OnValueChanged(float value)
    {
        displayText.text = value.ToString("0.#");
        switch (type)
        {
            case VolumeType.MAIN:
                AudioManager.audioManager.MainVolume = value;
                break;
            case VolumeType.BGM:
                AudioManager.audioManager.BgmVolume = value;
                break;
            case VolumeType.SFX:
                AudioManager.audioManager.SfxVolume = value;
                break;
            default:
                break;
        }
    }

    private void SetValue(float value)
    {
        slider.value = value;
        displayText.text = value.ToString("0.#");
    }
}
