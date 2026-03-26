using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour // Handles WASD movement and Shift running
{
    [SerializeField] private float moveSpeed = 6f; // Normal walking speed
    [SerializeField] private float runSpeed = 10f; // Faster running speed
    [SerializeField] private Rigidbody2D rb; // Reference to Rigidbody2D for movement
    [SerializeField] private PlayerState state; // Reference to the PlayerState script

    private Vector2 moveDir = Vector2.zero; // Stores current movement direction
    private InputAction moveAction; // Input action for WASD
    private InputAction runAction; // Input action for Shift

    private void Awake() // Runs when the script first loads
    {
        moveAction = new InputAction("Move", InputActionType.Value); // Creates movement input
        moveAction.AddCompositeBinding("2DVector") // Makes WASD into one 2D movement vector
            .With("Up", "<Keyboard>/w") // W moves up
            .With("Down", "<Keyboard>/s") // S moves down
            .With("Left", "<Keyboard>/a") // A moves left
            .With("Right", "<Keyboard>/d"); // D moves right

        moveAction.performed += ctx => // Runs while movement input is pressed
        {
            moveDir = ctx.ReadValue<Vector2>(); // Reads the movement direction

            if (moveDir != Vector2.zero && state != null) // If player is moving and state exists
            {
                state.lastMoveDir = moveDir; // Save the last movement direction
            }
        };

        moveAction.canceled += ctx => moveDir = Vector2.zero; // Stops movement when keys are released

        runAction = new InputAction("Run", binding: "<Keyboard>/leftShift"); // Shift key for running
    }

    private void OnEnable() // Enables input when object becomes active
    {
        moveAction.Enable(); // Turn on movement input
        runAction.Enable(); // Turn on run input
    }

    private void OnDisable() // Disables input when object becomes inactive
    {
        moveAction.Disable(); // Turn off movement input
        runAction.Disable(); // Turn off run input
    }

    private void FixedUpdate() // Runs during physics updates
    {
        if (rb == null || state == null) // Make sure references are assigned
        {
            return; // Stop if something is missing
        }

        if (!state.canMove) // If movement is disabled
        {
            rb.linearVelocity = Vector2.zero; // Stop the player
            state.isMoving = false; // Mark as not moving
            state.isRunning = false; // Mark as not running
            return; // Exit early
        }

        state.isMoving = moveDir != Vector2.zero; // True if movement keys are being pressed
        state.isRunning = runAction.IsPressed() && state.isMoving; // True if Shift is held while moving

        float currentSpeed = state.isRunning ? runSpeed : moveSpeed; // Choose running or walking speed
        rb.linearVelocity = moveDir.normalized * currentSpeed; // Move the player
    }
}
