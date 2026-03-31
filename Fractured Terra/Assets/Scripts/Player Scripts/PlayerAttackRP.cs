using UnityEngine;

public class PlayerAttackRP : MonoBehaviour
{
    public GameObject slashEffectPrefab;
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask breakableLayer;

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

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, breakableLayer);

        if (hit != null)
        {
            BreakableVase vase = hit.GetComponent<BreakableVase>();

            if (vase != null)
            {
                vase.Break();
            }
            else
            {
                Destroy(hit.gameObject);
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
