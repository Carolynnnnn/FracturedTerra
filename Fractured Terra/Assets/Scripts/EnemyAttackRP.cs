using System.Collections;
using UnityEngine;

public class EnemyAttackRP : MonoBehaviour
{
    public float attackRange = 1f; // how close player needs to be to attack
    public float attackDelay = 1.5f; // delay before attack actually hits (gives a bit of reaction time)
    public float damage = 10f; // damage dealt to player

    private Transform player;
    private PlayerHealth playerHealth;
    private PlayerController playerController;

    private bool isAttacking = false; // stops multiple attack coroutines from stacking

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player"); // finds player in scene

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

        // if player is paused / can’t move, stop attacking (used for menus, cutscenes, etc)
        if (playerController != null && !playerController.CanMove)
        {
            isAttacking = false;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        // if player is in range, start attack
        if (distance <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        float timer = 0f;

        // wait for attack delay (kind of like wind-up before hitting player)
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
                yield break; // cancels attack if player walks away
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // after delay, actually deal damage if still in range
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
                playerHealth.TakeDamage(damage); // hits player
            }
        }

        isAttacking = false; // allows next attack
    }
}