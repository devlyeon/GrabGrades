using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private bool isLoop = false;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    private float volume = 1.0f;

    public float Volume
    { 
        get => volume;
        set
        {
            volume = value;
            audioSource.volume = volume;
        }
    }

    void Awake()
    {
        audioSource.loop = isLoop;
    }

    public bool Play(int id)
    {
        if (audioClips.Count < id) return false;

        audioSource.clip = audioClips[id];
        if (isLoop) audioSource.Play();
        else audioSource.PlayOneShot(audioClips[id], volume);

        return true;
    }
}