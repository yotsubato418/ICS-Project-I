using UnityEngine;

public class HookDurability : MonoBehaviour
{
    [SerializeField] float maxDurability = 100f;
    [SerializeField] float currentDurability;

    public float CurrentDurability => currentDurability;

    void Awake()
    {
        currentDurability = maxDurability;
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f || currentDurability <= 0f)
            return;

        currentDurability = Mathf.Max(0f, currentDurability - amount);
        Debug.Log("Hook durability: " + currentDurability, this);

        if (currentDurability == 0f)
            Debug.Log("Hook is broken!", this);
    }
}