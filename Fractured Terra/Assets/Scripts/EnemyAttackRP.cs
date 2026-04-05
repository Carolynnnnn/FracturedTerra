using System.Collections;
using UnityEngine;

public class EnemyAttackRP : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackDelay = 1.5f;
    public float damage = 10f;

    private Transform player;
    private PlayerHealth playerHealth;
    private PlayerController playerController;

    private bool isAttacking = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
            playerController = playerObj.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (player == null || playerHealth == null) return;

        if (playerController != null && !playerController.CanMove)
        {
            isAttacking = false;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        float timer = 0f;

        while (timer < attackDelay)
        {
            if (player == null)
            {
                isAttacking = false;
                yield break;
            }

            if (playerController != null && !playerController.CanMove)
            {
                isAttacking = false;
                yield break;
            }

            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > attackRange)
            {
                isAttacking = false;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if (player != null)
        {
            if (playerController != null && !playerController.CanMove)
            {
                isAttacking = false;
                yield break;
            }

            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= attackRange)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        isAttacking = false;
    }
}