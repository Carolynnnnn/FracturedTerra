using System.Collections;
using UnityEngine;

public class FinalBossControllerRP : MonoBehaviour
{
    public float moveSpeed = 2f;

    [Header("Attack Settings")]
    public float closeAttackRange = 1f;
    public float farAttackRange = 3f;
    public float attackDamage = 15f;
    public float attackPause = 2f;
    public float spawnFallDuration = 1.2f;

    private Transform player;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;
    private Animator animator;

    private bool canAct = false;
    private bool isBusy = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(BossLoop());
    }

    IEnumerator BossLoop()
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("Fall");
        }

        yield return new WaitForSeconds(spawnFallDuration);
        canAct = true;

        while (true)
        {
            float chaseTime = 0f;
            while (chaseTime < 4f && !isBusy)
            {
                MoveTowardPlayer();
                chaseTime += Time.deltaTime;
                yield return null;
            }

            StopMoving();
            yield return new WaitForSeconds(2f);

            yield return StartCoroutine(DoAttack(closeAttackRange));
            yield return new WaitForSeconds(attackPause);

            yield return StartCoroutine(DoAttack(closeAttackRange));
            yield return new WaitForSeconds(attackPause);

            yield return StartCoroutine(DoAttack(farAttackRange));
            yield return new WaitForSeconds(attackPause);
        }
    }

    void MoveTowardPlayer()
    {
        if (!canAct || isBusy || player == null) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        if (animator != null)
        {
            animator.SetBool("IsRunning", true);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            if (direction.x < 0) sr.flipX = true;
            else if (direction.x > 0) sr.flipX = false;
        }
    }

    void StopMoving()
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", false);
        }
    }

    IEnumerator DoAttack(float range)
    {
        if (isBusy) yield break;

        isBusy = true;
        StopMoving();

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        yield return new WaitForSeconds(0.5f);

        if (player != null && playerHealth != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= range)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        yield return new WaitForSeconds(0.5f);
        isBusy = false;
    }
}