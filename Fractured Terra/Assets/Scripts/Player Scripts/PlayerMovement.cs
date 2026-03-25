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
