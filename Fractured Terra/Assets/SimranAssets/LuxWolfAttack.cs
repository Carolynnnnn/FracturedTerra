using UnityEngine;

// Attach to wolf enemy GameObjects in Simran_Level5 as a scene-level override.
// Mirrors Alisha's EnemyAttack pattern: uses OnTriggerStay2D on the wolf's
// AttackRange trigger child, fired via the root Rigidbody2D, to deal damage
// to the player when they get close.
public class LuxWolfAttack : MonoBehaviour
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
        }
    }
}
