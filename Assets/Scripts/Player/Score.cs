using UnityEngine;
using TMPro;

public class Score : MonoBehaviour

{   public int score = 0;  
    public TextMeshProUGUI scoreText;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.fontSize = 20;
        scoreText.color = Color.red;
        scoreText.fontStyle = FontStyles.Bold;
        score = 0;
        scoreText.text = "Score: " + score;
        resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
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
            scoreText.text = "Score: " + score;
        }
    }

    public void ShowResult()
    {
        int best = PlayerPrefs.GetInt("Best", 0);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("Best", best);
        }
        resultText.text = "Score: " + score + "\nBest: " + best;
        resultPanel.SetActive(true);
    }
}