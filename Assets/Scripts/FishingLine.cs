using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FishingLine : MonoBehaviour
{
    [SerializeField] Transform rodTip;
    [SerializeField] Transform hook;

    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.useWorldSpace = true;
        line.startWidth = 0.03f;
        line.endWidth = 0.03f;
    }

    void LateUpdate()
    {
        if (rodTip == null || hook == null)
            return;

        line.SetPosition(0, rodTip.position);
        line.SetPosition(1, hook.position);
    }
}