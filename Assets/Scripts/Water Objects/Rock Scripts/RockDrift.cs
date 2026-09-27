using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RockDrift : MonoBehaviour
{
    [SerializeField] float currentSpeed = 0.35f;
    [SerializeField] float bobHeight = 0.12f;
    [SerializeField] float bobCyclesPerSecond = 0.4f;
    [SerializeField] float despawnX = 12f;

    Rigidbody2D body;
    float startingY;
    float elapsed;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        startingY = body.position.y;
    }

    void FixedUpdate()
    {
        elapsed += Time.fixedDeltaTime;

        Vector2 nextPosition = new Vector2(
            body.position.x + currentSpeed * Time.fixedDeltaTime,
            startingY + bobHeight *
                Mathf.Sin(elapsed * bobCyclesPerSecond * 2f * Mathf.PI)
        );

        body.MovePosition(nextPosition);

        if (Mathf.Abs(nextPosition.x) > despawnX)
            Destroy(gameObject);
    }
}