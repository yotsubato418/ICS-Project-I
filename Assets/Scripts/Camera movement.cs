using UnityEngine;

public class Cameramovement : MonoBehaviour
{
    private Transform hook;
    private HookMovement hookMovement;
    private Vector3 tempPos;
    private float startY;

    void Start()
    {
        GameObject hookObj = GameObject.FindWithTag("Hook");
        hook = hookObj.transform;
        hookMovement = hookObj.GetComponent<HookMovement>();
        startY = transform.position.y;

    }

    // Update is called once per frame
    void FixedUpdate()  
    {
        tempPos = transform.position;

        if (hookMovement != null && hookMovement.casting)
        {
            if (hook.position.y < startY)
                tempPos.y = hook.position.y;
        }
        else
        {
            tempPos.y = startY;
        }

        transform.position = tempPos;
    }
    //reset at the top once dying
    public void ResetPosition()
    {
        Vector3 pos = transform.position;
        pos.y = startY;
        transform.position = pos;
    }
}
