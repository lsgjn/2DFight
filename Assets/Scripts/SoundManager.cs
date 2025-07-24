using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip KatanaSlashClip;
    public AudioClip LongSwordSlashClip;
    public AudioClip NormalswordClip;
    public AudioClip guardClip;
    public AudioClip dashClip;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
        }
        else
        {
            Destroy(gameObject); // 중복 제거
        }
    }


    public void Play(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void PlayKatanaSlash()
    {
        Play(KatanaSlashClip);
    }
    public void PlayLongSwordSlash()
    {
        Play(LongSwordSlashClip);
    }
    public void PlayNormalsword()
    {
        Play(NormalswordClip);
    }
    public void PlayGuard()
    {
        Play(guardClip);
    }
    public void PlayDash()
    {
        Play(dashClip);
    }
}
