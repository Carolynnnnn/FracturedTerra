using UnityEngine; 
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour // Handles jumping with Space
{
    [SerializeField] private Rigidbody2D rb; // Reference to the player's Rigidbody2D
    [SerializeField] private PlayerState state; // Reference to the PlayerState script
    [SerializeField] private float jumpForce = 8f; // Controls how strong the jump is
    [SerializeField] private bool isGrounded = true; // Simple check for whether player is on ground

    private InputAction jumpAction; // Input action for Space jump
}
