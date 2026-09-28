using UnityEngine;
using TMPro;

public class Score : MonoBehaviour

{   public int score = 0;
    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
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
            scoreText.text = "Score: " + score;
            Debug.Log("Score: " + score);
        }
    }
}
