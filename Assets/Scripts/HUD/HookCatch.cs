using UnityEngine;

public class HookCatch : MonoBehaviour
{
    public SmoothBar durabilityBar;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.CompareTag("Rock"))
        {
            durabilityBar.SetValue(durabilityBar.currentValue - 10f);
        }
    }
}
