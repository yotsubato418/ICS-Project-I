using UnityEngine;

public class Cameramovement : MonoBehaviour
{
    private Transform hook; 

    private Vector3 tempPos;
    private float startY;

    void Start()
    {
        hook = GameObject.FindWithTag("Hook").transform;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempPos = transform.position;
        if (hook.position.y < startY)
            tempPos.y = hook.position.y;

        transform.position = tempPos;
    }
}
