using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private bool isLoop = false;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    private float volume = 10.0f;

    void Awake()
    {
        audioSource.loop = isLoop;

        // 테스트용
        if (isLoop)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        this.volume = volume / 10.0f;
        audioSource.volume = this.volume;
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