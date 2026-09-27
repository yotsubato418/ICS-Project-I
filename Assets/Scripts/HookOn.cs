using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HookOn : MonoBehaviour
{
    [SerializeField] Transform catchPoint;

    Rigidbody2D hookBody;
    HashSet<FishSwim> caughtFish = new HashSet<FishSwim>();

    void Awake()
    {
        hookBody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryCatch(other);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        TryCatch(collision.collider);
    }

    void TryCatch(Collider2D other)
    {
        if (catchPoint == null)
            return;

        FishSwim fish = other.GetComponentInParent<FishSwim>();
        if (fish == null)
            return;

        Rigidbody2D fishBody = fish.GetComponent<Rigidbody2D>();
        Transform mouthPoint = fish.transform.Find("MouthPoint");

        if (fishBody == null || mouthPoint == null)
        {
            Debug.LogWarning(
                "Fish needs a Rigidbody2D and a child named MouthPoint.",
                fish
            );
            return;
        }

        // record both attachment points in their Rigidbody's local space.
        Vector2 fishAnchor =
            fish.transform.InverseTransformPoint(mouthPoint.position);
        Vector2 hookAnchor =
            hookBody.transform.InverseTransformPoint(catchPoint.position);

        if (!caughtFish.Add(fish))
            return;
        fish.enabled = false; // stop FishSwim moving it.

        fishBody.simulated = true;
        fishBody.bodyType = RigidbodyType2D.Dynamic;
        fishBody.gravityScale = 0.6f;
        fishBody.freezeRotation = false;
        fishBody.linearDamping = 1.5f;
        fishBody.angularDamping = 2f;
        fishBody.linearVelocity = Vector2.zero;
        fishBody.angularVelocity = 0f;

        // place its mouth at the hook once, when it is caught.
        fishBody.position +=
            (Vector2)(catchPoint.position - mouthPoint.position);

        HingeJoint2D joint = fish.gameObject.AddComponent<HingeJoint2D>();
        joint.connectedBody = hookBody;
        joint.autoConfigureConnectedAnchor = false;
        joint.useConnectedAnchor = true;
        joint.anchor = fishAnchor;
        joint.connectedAnchor = hookAnchor;
        joint.enableCollision = false;
    }
}