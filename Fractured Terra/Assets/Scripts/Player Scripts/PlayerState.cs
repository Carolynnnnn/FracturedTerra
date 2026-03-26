using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public bool canMove = true; // Lets other systems stop movement if needed
    public bool isMoving = false; // Tracks whether the player is moving
    public bool isRunning = false; // Tracks whether the player is running
    public bool isJumping = false; // Tracks whether the player is jumping

    public Vector2 lastMoveDir = Vector2.down; // Stores the last movement direction

}