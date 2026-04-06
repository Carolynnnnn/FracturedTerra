using UnityEngine;

public class EnemyChaseRP : MonoBehaviour
{
    public float moveSpeed = 2f; // how fast the enemy moves toward the player
    public float stopDistance = 0.9f; // how close it gets before stopping (so it doesn’t overlap player)

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player"); // finds player in scene

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerController = playerObj.GetComponent<PlayerController>();
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // stop everything if player is paused / can’t move (same idea as attack script)
        if (playerController != null && !playerController.CanMove)
        {
            if (animator != null)
                animator.SetBool("IsMoving", false);

            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized; // direction toward player
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            // moves enemy toward player
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

            if (animator != null)
            {
                animator.SetFloat("MoveX", direction.x); // updates animation direction
                animator.SetFloat("MoveY", direction.y);
                animator.SetBool("IsMoving", true);
            }

            // flips sprite left/right depending on movement
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (direction.x < 0) sr.flipX = true;
                if (direction.x > 0) sr.flipX = false;
            }
        }
        else
        {
            // stop moving animation when close enough to attack
            if (animator != null)
                animator.SetBool("IsMoving", false);
        }
    }
}