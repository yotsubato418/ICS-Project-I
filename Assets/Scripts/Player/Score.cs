using UnityEngine;

public class Score : MonoBehaviour

{   public int score = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            score += 1;
            Debug.Log("Score: " + score);
        }
    }
}
