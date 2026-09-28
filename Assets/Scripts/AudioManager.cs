using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;  
    public AudioSource sfxSource;   

    [Header("Clips")]
    public AudioClip music;
    public AudioClip splash;
    public AudioClip catchFish;
    public AudioClip bump;
    public AudioClip reelUp;
    public AudioClip happy;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.volume = 0.4f;
        musicSource.Play();
    }

    public void Play(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}