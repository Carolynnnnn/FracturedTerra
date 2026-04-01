using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject slashEffectPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int damage = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (slashEffectPrefab != null && attackPoint != null)
        {
            Instantiate(slashEffectPrefab, attackPoint.position, Quaternion.identity);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyHealth enemy = enemyCollider.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log(enemy.gameObject.name + " took damage from O attack");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}