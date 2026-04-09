using System.Collections;
using UnityEngine;

public class FinalBossControllerRP : MonoBehaviour
{
    public float moveSpeed = 2f; // how fast the boss chases the player

    [Header("Attack Settings")]
    public float closeAttackRange = 1f; // range for the close attacks
    public float farAttackRange = 3f; // range for the bigger / farther attack
    public float attackDamage = 15f; // damage boss does per hit
    public float attackPause = 2f; // pause between attacks so it’s not constant
    public float spawnFallDuration = 1.2f; // how long the spawn/fall intro takes

    private Transform player;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;
    private Animator animator;

    private bool canAct = false; // boss stays inactive during spawn intro
    private bool isBusy = false; // stops movement while attacking

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player"); // finds player in scene

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(BossLoop()); // starts the boss fight pattern
    }

    IEnumerator BossLoop()
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("Fall"); // spawn intro animation when boss drops in
        }

        yield return new WaitForSeconds(spawnFallDuration);
        canAct = true; // boss can start fighting after intro

        while (true)
        {
            float chaseTime = 0f;
            while (chaseTime < 4f && !isBusy)
            {
                MoveTowardPlayer(); // boss chases player for a few seconds
                chaseTime += Time.deltaTime;
                yield return null;
            }

            StopMoving();
            yield return new WaitForSeconds(2f); // short pause before attack pattern starts

            yield return StartCoroutine(DoAttack(closeAttackRange)); // first close hit
            yield return new WaitForSeconds(attackPause);

            yield return StartCoroutine(DoAttack(closeAttackRange)); // second close hit
            yield return new WaitForSeconds(attackPause);

            yield return StartCoroutine(DoAttack(farAttackRange)); // wider/farther attack
            yield return new WaitForSeconds(attackPause);
        }
    }

    void MoveTowardPlayer()
    {
        if (!canAct || isBusy || player == null) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition); // moves boss toward player

        if (animator != null)
        {
            animator.SetBool("IsRunning", true); // plays run animation while chasing
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            if (direction.x < 0) sr.flipX = true;
            else if (direction.x > 0) sr.flipX = false; // flips boss to face player
        }
    }

    void StopMoving()
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", false); // stops run animation
        }
    }

    IEnumerator DoAttack(float range)
    {
        if (isBusy) yield break;

        isBusy = true;
        StopMoving(); // boss stops before attacking

        if (animator != null)
        {
            animator.SetTrigger("Attack"); // plays attack animation
        }

        yield return new WaitForSeconds(0.5f); // lines damage up with the animation a bit better

        if (player != null && playerHealth != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= range)
            {
                playerHealth.TakeDamage(attackDamage); // only damages player if they’re close enough
            }
        }

        yield return new WaitForSeconds(0.5f); // small recovery after attack
        isBusy = false;
    }
}