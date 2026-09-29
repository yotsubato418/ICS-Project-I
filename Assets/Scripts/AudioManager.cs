using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource reelSource;

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
    public void PlayReel()
    {
        reelSource.clip = reelUp;
        reelSource.Play();
    }
    public void StopReel()
    {
        reelSource.Stop();
    }
}