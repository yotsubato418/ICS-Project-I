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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            score += 1;
            Debug.Log("Score: " + score);
        }
    }
}
