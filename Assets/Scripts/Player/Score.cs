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
<<<<<<< HEAD
            score += 1;
            scoreText.text = "Score: " + score;
=======
            if (other.name.Contains("1"))
                score += 1;
            if (other.name.Contains("2"))
                score += 2;
            if (other.name.Contains("3"))
                score += 3;
            if (other.name.Contains("4"))
                score += 4;
            if (other.name.Contains("og"))
                score += 5;
            if (other.name.Contains("secret"))
                score += 10;
            
            Debug.Log("Score: " + score);
>>>>>>> 7040fe1fe111b90b9520b1f8c86f4c1ce5c8b637
        }
    }
}
