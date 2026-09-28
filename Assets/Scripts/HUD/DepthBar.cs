using UnityEngine;
using UnityEngine.UI;

public class DepthBar : MonoBehaviour
{
    public Slider bar;
    Transform hook;
    [SerializeField] float minDepthY = 3f;
    [SerializeField] float maxDepthY = -10f;
    void Start()
    {
        hook = GameObject.FindWithTag("Hook").transform;
    }

    // Update is called once per frame
    void Update()
    {
        bar.value = Mathf.InverseLerp(minDepthY, maxDepthY, hook.position.y);
    }
}
