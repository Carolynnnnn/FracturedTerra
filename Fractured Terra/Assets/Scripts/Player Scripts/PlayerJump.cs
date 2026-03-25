using UnityEngine; 
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour // Handles jumping with Space
{
    [SerializeField] private Rigidbody2D rb; // Reference to the player's Rigidbody2D
    [SerializeField] private PlayerState state; // Reference to the PlayerState script
    [SerializeField] private float jumpForce = 8f; // Controls how strong the jump is
    [SerializeField] private bool isGrounded = true; // Simple check for whether player is on ground

    private InputAction jumpAction; // Input action for Space jump
    
    private void Awake() // Runs when the script first loads
    {
        jumpAction = new InputAction("Jump", binding: "<Keyboard>/space"); // Creates Space key input
        jumpAction.performed += ctx => Jump(); // Calls Jump() when Space is pressed
    }


    private void OnEnable() // Enables jump input when object becomes active
    {
        jumpAction.Enable(); // Turn on jump input
    }


    private void OnDisable() // Disables jump input when object becomes inactive
    {
        jumpAction.Disable(); // Turn off jump input
    }

}
