// public class SoundManager : MonoBehaviour
// {
//     public static SoundManager Instance;

//     public AudioClip hitClip;
//     public AudioClip parrySuccessClip;
//     public AudioClip parryFailClip;
//     public AudioClip deathClip;
//     public AudioClip jumpClip;

//     private AudioSource audioSource;

//     void Awake()
//     {
//         if (Instance == null) Instance = this;
//         else Destroy(gameObject);

//         audioSource = GetComponent<AudioSource>();
//     }

//     public void Play(AudioClip clip)
//     {
//         if (clip != null)
//             audioSource.PlayOneShot(clip);
//     }

//     public void PlayHit() => Play(hitClip);
//     public void PlayParrySuccess() => Play(parrySuccessClip);
//     public void PlayDeath() => Play(deathClip);
// }
