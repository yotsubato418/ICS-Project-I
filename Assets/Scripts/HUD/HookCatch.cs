using UnityEngine;

public class HookCatch : MonoBehaviour
{
    public SmoothBar hpBar;
    public SmoothBar durabilityBar;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            durabilityBar.SetValue(durabilityBar.currentValue - 5f);
        }
        if (other.CompareTag("Rock"))
        {
            hpBar.SetValue(hpBar.currentValue - 10f);
        }
    }
}
