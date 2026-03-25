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

}


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


        if (moveDir != Vector2.zero && state != null) // If the player is moving and state exists
        {
            state.lastMoveDir = moveDir; // Save the last movement direction
        }
    };


    moveAction.canceled += ctx => moveDir = Vector2.zero; // Stops movement when keys are released


    runAction = new InputAction("Run", binding: "<Keyboard>/leftShift"); // Shift key for running
}

