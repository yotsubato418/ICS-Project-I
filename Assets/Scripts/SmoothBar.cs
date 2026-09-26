using UnityEngine;
using UnityEngine.UI;

public class SmoothBar : MonoBehaviour
{
    public Transform fillTransform;
    public float maxValue = 100f;
    public float currentValue = 100f;
    public float smoothSpeed = 2f;
    float fullWidth;
    void Start()
    {
        fullWidth = fillTransform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float targetFill = fullWidth * (currentValue / maxValue);
        float newScale = Mathf.MoveTowards(fillTransform.localScale.x, targetFill, smoothSpeed * Time.deltaTime);
        fillTransform.localScale = new Vector3(newScale, fillTransform.localScale.y, fillTransform.localScale.z);
    }

    public void SetValue(float newValue)
    {
        currentValue = Mathf.Clamp(newValue, 0f, maxValue);
    }

}
