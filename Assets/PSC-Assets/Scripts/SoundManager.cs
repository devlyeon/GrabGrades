using UnityEngine;

public enum SoundType
{
    GetSFX,
    LostSFX,
    BGM
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip getClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private AudioClip BgmClip;   // 메인 BGM 파일


    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    
    void Awake()
    {
        //if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        //else Destroy(gameObject);
        instance = this;
    }
    

    // 효과음 재생
    public void PlaySFX(SoundType type, float volume = 1f)
    {
        AudioClip targetClip = null;
        switch (type)
        {
            case SoundType.GetSFX: targetClip = getClip; break;
            case SoundType.LostSFX: targetClip = loseClip; break;
        }
        if (targetClip != null)
        {
            float audioVolume = volume;
            if (AudioManager.audioManager != null)
            {
                audioVolume *= AudioManager.audioManager.SfxVolume;
            }
            sfxSource.PlayOneShot(targetClip, audioVolume);
        }
    }

    // 배경음 재생
    public void PlayBGM(SoundType type, bool loop = true)
    {
        AudioClip targetClip = null;

        // 나중에 BGM 추가될 때 사용할 로직
        switch (type)
        {
            case SoundType.BGM:
                targetClip = BgmClip; break;
        }

        if (targetClip == null || bgmSource.clip == targetClip) return;

        bgmSource.clip = targetClip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }
    
    // 배경음 정지 기능 추가
    public void StopBGM() => bgmSource.Stop();
}