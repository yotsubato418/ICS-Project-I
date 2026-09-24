using UnityEngine;

public class Cameramovement : MonoBehaviour
{
    private Transform hook; 

    private Vector3 tempPos;

    void Start()
    {
        hook = GameObject.FindWithTag("Hook").transform;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = transform.position;
        tempPos.y = hook.position.y;

        transform.position = tempPos;
    }
}
