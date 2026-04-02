using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private float lastAttackTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();

        if (player != null && Time.time >= lastAttackTime + attackCooldown)
        {
            player.TakeDamage(damage);
            lastAttackTime = Time.time;

            Debug.Log("Player took damage from enemy");
        }
    }
}