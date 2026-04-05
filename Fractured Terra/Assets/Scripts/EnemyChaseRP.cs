using UnityEngine;

public class EnemyChaseRP : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float stopDistance = 0.9f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

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

        if (playerController != null && !playerController.CanMove)
        {
            if (animator != null)
                animator.SetBool("IsMoving", false);

            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

            if (animator != null)
            {
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
                animator.SetBool("IsMoving", true);
            }

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (direction.x < 0) sr.flipX = true;
                if (direction.x > 0) sr.flipX = false;
            }
        }
        else
        {
            if (animator != null)
                animator.SetBool("IsMoving", false);
        }
    }
}