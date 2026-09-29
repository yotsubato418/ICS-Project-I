using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Top3Best top3Best;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);
    }

    public void GameOverScreen()
    {
    Debug.Log("GAME OVER ACTIVATED!");

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
        else
            Debug.LogError("Game Over Screen is not assigned!");

    top3Best.Savescore();


    }
}
