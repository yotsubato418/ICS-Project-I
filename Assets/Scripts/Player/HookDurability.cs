using UnityEngine;

public class HookDurability : MonoBehaviour
{
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

        if (currentDurability == 0f)
        {
            Debug.Log("Hook is broken!", this);
            AudioManager.Instance.Play(AudioManager.Instance.broken); 
        }
    }
    public void ResetDurability()
    {
        currentDurability = maxDurability;
        if (durabilityBar != null)
        {
            durabilityBar.localScale = new Vector3(fullWidth, durabilityBar.localScale.y, 1f);
        }
    }
}