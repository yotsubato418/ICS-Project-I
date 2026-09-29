using UnityEngine;

public class HookDurability : MonoBehaviour
{
    private GameOver gameOver;

    [SerializeField] float maxDurability = 100f;
    [SerializeField] float currentDurability;
    [SerializeField] Transform durabilityBar;
    float fullWidth;

    public float CurrentDurability => currentDurability;

    void Awake()
    {
        currentDurability = maxDurability;
        if (durabilityBar != null)
        {
            fullWidth = durabilityBar.localScale.x;
        }
        gameOver = FindFirstObjectByType<GameOver>();
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f || currentDurability <= 0f)
            return;

        currentDurability = Mathf.Max(0f, currentDurability - amount);
        if (durabilityBar != null)
        {
            durabilityBar.localScale = new Vector3(fullWidth * currentDurability / maxDurability, durabilityBar.localScale.y, 1f);
        }
        Debug.Log("Hook durability: " + currentDurability, this);

        if (currentDurability <= 0f)
        {
              if (gameOver == null){
            Debug.LogError("GAMEOVER REFERENCE IS NULL!");
            return;
            }

            gameOver.GameOverScreen();
        }
            
    }
}