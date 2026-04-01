using UnityEngine;

public class PlayerAttackRP : MonoBehaviour
{
    public GameObject slashEffectPrefab;
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask breakableLayer;
    public LayerMask enemyLayer;
    public int damage = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (slashEffectPrefab != null && attackPoint != null)
        {
            GameObject slash = Instantiate(slashEffectPrefab, attackPoint.position, Quaternion.identity);
            Destroy(slash, 0.3f);
        }

        // Hit breakable objects
        Collider2D breakableHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, breakableLayer);

        if (breakableHit != null)
        {
            BreakableVase vase = breakableHit.GetComponent<BreakableVase>();

            if (vase != null)
            {
                vase.Break();
            }
            else
            {
                Destroy(breakableHit.gameObject);
            }
        }

        // Hit enemies
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemyHit in enemyHits)
        {
            EnemyHealth enemy = enemyHit.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log(enemy.gameObject.name + " took damage from player attack.");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}