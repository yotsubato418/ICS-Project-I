using UnityEngine;

public class BubbleSounds : MonoBehaviour
{
    public AudioSource source;        
    public AudioClip[] bubbleClips;   
    public float minDelay = 2f;     
    public float maxDelay = 6f;      

    float timer;

    void Start()
    {
        timer = Random.Range(minDelay, maxDelay);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PlayRandomBubble();
            timer = Random.Range(minDelay, maxDelay);
        }
    }

    void PlayRandomBubble()
    {
        if (bubbleClips.Length == 0) return;

        AudioClip clip = bubbleClips[Random.Range(0, bubbleClips.Length)];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(clip, Random.Range(0.5f, 1f));
    }
}