using UnityEngine;

// Drop-in replacement for EnemyAttack on the Wolf's AttackRange child.
// Uses GetComponentInParent so PlayerHealth is found even when the
// colliding object is a child of the Player (e.g. the "Character" object).
public class WolfAttack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private float lastAttackTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponentInParent<PlayerHealth>();

        if (player != null && Time.time >= lastAttackTime + attackCooldown)
        {
            player.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}
