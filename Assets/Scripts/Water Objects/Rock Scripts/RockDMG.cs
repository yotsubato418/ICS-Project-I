using UnityEngine;

public class RockDMG : MonoBehaviour
{
    [SerializeField] float damage = 15f;
    [SerializeField] float speedDamage = 3f;
    [SerializeField] float bounceImpulse = 3f;

    Rigidbody2D rockBody;
    bool hasHitHook;

    void Awake()
    {
        rockBody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryHitHook(other);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        TryHitHook(collision.collider);
    }

    void TryHitHook(Collider2D other)
    {
        if (hasHitHook)
            return;

        HookDurability durability = other.GetComponentInParent<HookDurability>();

        if (durability == null)
            return;

        if (durability.GetComponent<HookMovement>().recalling)
            return;

        hasHitHook = true;
        Rigidbody2D hookBody = durability.GetComponent<Rigidbody2D>();
        durability.TakeDamage(damage + hookBody.linearVelocity.magnitude * speedDamage);

        if (hookBody == null ||
            hookBody.bodyType != RigidbodyType2D.Dynamic)
            return;

        // push the hook away from the rock.
        Vector2 direction = hookBody.position - rockBody.position;

        if (direction.sqrMagnitude < 0.001f)
            direction = -hookBody.linearVelocity;

        if (direction.sqrMagnitude < 0.001f)
            direction = Vector2.up;

        hookBody.AddForce(
            direction.normalized * bounceImpulse,
            ForceMode2D.Impulse
        );
    }
}