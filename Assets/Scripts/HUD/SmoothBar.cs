using UnityEngine;

public class SmoothBar : MonoBehaviour
{
    public float maxValue = 100f;
    public float currentValue = 100f;
    public float smoothSpeed = 2f;
    float fullWidth;
    void Start()
    {
        fullWidth = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float targetFill = fullWidth * (currentValue / maxValue);
        float newScale = Mathf.MoveTowards(transform.localScale.x, targetFill, smoothSpeed * Time.deltaTime);
        transform.localScale = new Vector3(newScale, transform.localScale.y, transform.localScale.z);
    }

    public void SetValue(float newValue)
    {
        currentValue = Mathf.Clamp(newValue, 0f, maxValue);
    }

}
