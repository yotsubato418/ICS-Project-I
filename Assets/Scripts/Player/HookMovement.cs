using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class HookMovement : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] float hookMass = 1f;
    [SerializeField] float horizontalAcceleration = 15f;
    [SerializeField] float descentAcceleration = 6f;
    [SerializeField] float ascentAcceleration = 12f;
    [SerializeField] float returnAcceleration = 30f;
    [SerializeField] float waterDrag = 3f;

    [Header("Movement Limits")]
    [SerializeField] float leftLimit = -4f;
    [SerializeField] float rightLimit = 4f;
    [SerializeField] float surfaceY = 3f;
    [SerializeField] float bottomY = -3f;
    [SerializeField] float mouseSteeringSensitivity = 2f;

    enum ControlMode { Mouse, AD, Arrows }
    ControlMode controlMode = ControlMode.Mouse;

    const float mouseSwitchPixels = 2f;

    Rigidbody2D body;
    HookOn hookCatch;
    Camera mainCamera;
    float horizontalInput;
    float mouseTargetX;
    Vector2 lastMousePosition;
    bool hasMousePosition;
    bool casting;
    bool recalling;
    HookDurability durability;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        hookCatch = GetComponent<HookOn>();
        body.bodyType = RigidbodyType2D.Kinematic;
        body.useAutoMass = false;
        body.mass = Mathf.Max(0.01f, hookMass);
        body.gravityScale = 0f;
        body.freezeRotation = true;

        durability = GetComponent<HookDurability>();

        mainCamera = Camera.main;
        mouseTargetX = body.position.x;
    }

    void Update()
    {
        if (casting && durability.CurrentDurability <= 0f)
        {
            recalling = true;
        }
        Mouse mouse = Mouse.current;

        if (mouse != null && mainCamera != null)
        {
            Vector2 mousePosition = mouse.position.ReadValue();

            if (hasMousePosition &&
                (mousePosition - lastMousePosition).sqrMagnitude >
                mouseSwitchPixels * mouseSwitchPixels)
            {
                controlMode = ControlMode.Mouse;
            }

            lastMousePosition = mousePosition;
            hasMousePosition = true;
            mouseTargetX = mainCamera.ScreenToWorldPoint(mousePosition).x;
        }

        Keyboard keyboard = Keyboard.current;
        horizontalInput = 0f;

        if (keyboard == null)
            return;

        if (keyboard.aKey.wasPressedThisFrame ||
            keyboard.dKey.wasPressedThisFrame)
        {
            controlMode = ControlMode.AD;
        }

        if (keyboard.leftArrowKey.wasPressedThisFrame ||
            keyboard.rightArrowKey.wasPressedThisFrame)
        {
            controlMode = ControlMode.Arrows;
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            if (!casting)
            {
                body.bodyType = RigidbodyType2D.Dynamic;
                casting = true;
                recalling = false;
            }
            else
            {
                recalling = true;
            }
        }

        if (controlMode == ControlMode.AD)
        {
            horizontalInput =
                (keyboard.dKey.isPressed ? 1f : 0f) -
                (keyboard.aKey.isPressed ? 1f : 0f);
        }
        else if (controlMode == ControlMode.Arrows)
        {
            horizontalInput =
                (keyboard.rightArrowKey.isPressed ? 1f : 0f) -
                (keyboard.leftArrowKey.isPressed ? 1f : 0f);
        }
    }

    void FixedUpdate()
    {
        EnforceLimits();

        if (!casting)
            return;

        float steering = horizontalInput;
        if (recalling)
        {
            if (body.position.x > 0.1f)
                steering = -1f;
            else if (body.position.x < -0.1f)
                steering = 1f;
            else
                steering = 0f;
        }
        else if (controlMode == ControlMode.Mouse)
        {
            float distanceToMouse = mouseTargetX - body.position.x;
            steering = Mathf.Clamp(
                distanceToMouse * mouseSteeringSensitivity, -1f, 1f
            );
        }

        // stop pushing outward when the hook reaches a side limit
        if ((body.position.x <= leftLimit && steering < 0f) ||
            (body.position.x >= rightLimit && steering > 0f))
        {
            steering = 0f;
        }

        float verticalForce;

        if (recalling)
        {
            float fishMass = 0f;
            float fishWeight = 0f;

            if (hookCatch != null)
                hookCatch.GetCaughtLoad(out fishMass, out fishWeight);

            verticalForce =
                (body.mass + fishMass) * ascentAcceleration + fishWeight;
        }
        else
        {
            verticalForce = -body.mass * descentAcceleration;
        }

        Vector2 movementForce = new Vector2(
            body.mass * steering * horizontalAcceleration,
            verticalForce
        );

        body.AddForce(movementForce, ForceMode2D.Force);

        // water resists motion in either direction.
        if (body.position.y < surfaceY)
        {
            Vector2 dragForce = -Mathf.Max(0f, waterDrag) *
                                body.linearVelocity;

            body.AddForce(dragForce, ForceMode2D.Force);
        }
    }

    void EnforceLimits()
    {
        Vector2 position = body.position;
        Vector2 velocity = body.linearVelocity;

        if (position.x < leftLimit)
        {
            position.x = leftLimit;
            velocity.x = Mathf.Max(0f, velocity.x);
        }
        else if (position.x > rightLimit)
        {
            position.x = rightLimit;
            velocity.x = Mathf.Min(0f, velocity.x);
        }
        
        if (recalling && Mathf.Abs(position.x) < 0.1f)
        {
            position.x = 0f;
            velocity.x = 0f;
        }

        if (casting && !recalling && position.y <= bottomY)
        {
            position.y = bottomY;
            velocity.y = 0f;
            recalling = true;
        }

        if (casting && recalling && position.y >= surfaceY)
        {
            position.y = surfaceY;
            casting = false;
            recalling = false;

            body.position = position;
            body.linearVelocity = Vector2.zero;
            body.angularVelocity = 0f;
            body.bodyType = RigidbodyType2D.Kinematic;
            return;
        }

        // these corrections only prevent crossing the game area's edges
        // movement between the edges comes from forces
        body.position = position;
        body.linearVelocity = velocity;
    }
}