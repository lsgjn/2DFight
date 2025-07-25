using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    //public AudioClip hitClip;
    public AudioClip parryClip;
    public AudioClip guardClip;
    //public AudioClip deathClip;
    public AudioClip jumpClip;
    public AudioClip dashClip;
    public AudioClip hurtClip; // 추가된 사운드 클립
    public AudioClip normalswordClip; // 추가된 사운드 클립
    public AudioClip mainClip;
    public AudioClip gameClip; // 게임 시작 사운드

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동에도 유지되게 할 경우
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
    public void PlayGuard() => Play(guardClip);
    public void PlayDash() => Play(dashClip);
    public void PlayNormalsword() => Play(normalswordClip);
    public void PlayParry() => Play(parryClip);
    public void PlayHurt() => Play(hurtClip);
    public void PlayJump() => Play(jumpClip);
    public void PlayMainLoop()
    {
        if (mainClip != null)
        {
            audioSource.clip = mainClip;
            audioSource.loop = true;
            audioSource.time = 0f;
            audioSource.Play();
        }
    }

    public void PlayGameLoop()
    {
        if (gameClip != null)
        {
            audioSource.clip = gameClip;
            audioSource.loop = true;
            audioSource.time = 0f;
            audioSource.Play();
        }
    }
}
