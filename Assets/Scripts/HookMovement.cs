using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class HookMovement : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = 5f;
    [SerializeField] float descentSpeed = 1.5f;
    [SerializeField] float ascentSpeed = 3f;
    [SerializeField] float leftLimit = -4f;
    [SerializeField] float rightLimit = 4f;
    [SerializeField] float surfaceY = 3f;
    [SerializeField] float bottomY = -3f;

    Rigidbody2D body;
    Camera mainCamera;
    float horizontalInput;
    float mouseTargetX;
    bool casting;
    bool recalling;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        body.gravityScale = 0f;

        mainCamera = Camera.main;
        mouseTargetX = body.position.x;
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        horizontalInput = 0f;

        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                horizontalInput -= 1f;

            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                horizontalInput += 1f;

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                if (!casting)
                {
                    casting = true;
                    recalling = false;
                }
                else
                {
                    recalling = true;
                }
            }
        }

        if (Mouse.current != null && mainCamera != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            mouseTargetX = mainCamera.ScreenToWorldPoint(mousePosition).x;
        }
    }

    void FixedUpdate()
    {
        if (!casting) return;

        float step = horizontalSpeed * Time.fixedDeltaTime;

        // A/D or arrow keys override mouse steering while held.
        float nextX = Mathf.Abs(horizontalInput) > 0.01f
            ? body.position.x + horizontalInput * step
            : Mathf.MoveTowards(body.position.x, mouseTargetX, step);

        float verticalSpeed = recalling ? ascentSpeed : -descentSpeed;
        float nextY = body.position.y + verticalSpeed * Time.fixedDeltaTime;

        if (nextY <= bottomY)
        {
            nextY = bottomY;
            recalling = true;
        }

        if (recalling && nextY >= surfaceY)
        {
            nextY = surfaceY;
            casting = false;
            recalling = false;
        }

        body.MovePosition(new Vector2(
            Mathf.Clamp(nextX, leftLimit, rightLimit),
            nextY
        ));
    }
}