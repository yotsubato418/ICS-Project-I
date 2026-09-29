using UnityEngine;
using TMPro;

public class DepthBarScript : MonoBehaviour
{
    public TMP_Text depthText;
    Transform hook;
    [SerializeField] float surfaceY = 3f;

    void Start()
    {
        depthText.color = Color.black;
        depthText.fontStyle = FontStyles.Bold;
        hook = GameObject.FindWithTag("Hook").transform;
    }

    void Update()
    {
        float depth = Mathf.Max(0f, surfaceY - hook.position.y);
        depthText.text = Mathf.RoundToInt(depth) + "m";
    }
}