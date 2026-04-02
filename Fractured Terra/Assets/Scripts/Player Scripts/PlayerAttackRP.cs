using System.Collections;
using UnityEngine;

public class PlayerAttackRP : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public LayerMask breakableLayer;
    public PlayerStatsRP playerStats;
    public PlayerDefenseRP playerDefense;

    [Header("Abilities")]
    public AbilityDataRP[] abilities = new AbilityDataRP[12];
    public int currentAbilityIndex = 0;

    [Header("Effect Settings")]
    public Vector2 effectOffset = Vector2.zero;
    public float effectLifetime = 0.5f;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TryUseCurrentAbility();
        }
    }

    void TryUseCurrentAbility()
    {
        if (abilities == null || abilities.Length == 0)
        {
            Debug.LogWarning("No abilities set on PlayerAttackRP.");
            return;
        }

        if (currentAbilityIndex < 0 || currentAbilityIndex >= abilities.Length)
        {
            Debug.LogWarning("Current ability index is out of range.");
            return;
        }

        AbilityDataRP currentAbility = abilities[currentAbilityIndex];

        if (currentAbility == null)
        {
            Debug.LogWarning("Current ability is null.");
            return;
        }

        if (!currentAbility.unlocked)
        {
            Debug.Log("Ability is locked.");
            return;
        }

        if (Time.time < nextAttackTime)
        {
            return;
        }

        UseAbility(currentAbility);
        nextAttackTime = Time.time + currentAbility.cooldown;
    }

    void UseAbility(AbilityDataRP ability)
    {
        switch (ability.type)
        {
            case AbilityType.Melee:
                UseMeleeAbility(ability);
                break;

            case AbilityType.Area:
                UseAreaAbility(ability);
                break;

            case AbilityType.Projectile:
                UseProjectileAbility(ability);
                break;

            case AbilityType.Freeze:
                UseFreezeAbility(ability);
                break;

            case AbilityType.Charm:
                UseCharmAbility(ability);
                break;

            case AbilityType.ShieldAura:
                UseShieldAuraAbility(ability);
                break;

            default:
                Debug.LogWarning("Unknown ability type on: " + ability.abilityName);
                break;
        }
    }

    void UseMeleeAbility(AbilityDataRP ability)
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("AttackPoint is missing.");
            return;
        }

        SpawnEffectAtPoint(ability, attackPoint.position);

        int finalDamage = GetFinalDamage(ability.damage);

        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            enemyLayer
        );

        foreach (Collider2D hit in enemyHits)
        {
            EnemyHealthRP enemy = hit.GetComponent<EnemyHealthRP>();
            if (enemy != null)
            {
                enemy.TakeDamage(finalDamage);
            }
        }

        Collider2D[] breakHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            breakableLayer
        );

        foreach (Collider2D hit in breakHits)
        {
            if (hit.CompareTag("breakitem"))
            {
                Destroy(hit.gameObject);
            }
        }
    }

    void UseAreaAbility(AbilityDataRP ability)
    {
        SpawnEffectAtPoint(ability, transform.position);

        int finalDamage = GetFinalDamage(ability.damage);

        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            transform.position,
            ability.range,
            enemyLayer
        );

        foreach (Collider2D hit in enemyHits)
        {
            EnemyHealthRP enemy = hit.GetComponent<EnemyHealthRP>();
            if (enemy != null)
            {
                enemy.TakeDamage(finalDamage);
            }
        }

        Collider2D[] breakHits = Physics2D.OverlapCircleAll(
            transform.position,
            ability.range,
            breakableLayer
        );

        foreach (Collider2D hit in breakHits)
        {
            if (hit.CompareTag("breakitem"))
            {
                Destroy(hit.gameObject);
            }
        }
    }

    void UseProjectileAbility(AbilityDataRP ability)
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("AttackPoint is missing.");
            return;
        }

        if (ability.effectPrefab == null)
        {
            Debug.LogWarning("Projectile prefab missing for " + ability.abilityName);
            return;
        }

        GameObject projectileObj = Instantiate(
            ability.effectPrefab,
            attackPoint.position + (Vector3)effectOffset,
            Quaternion.identity
        );

        AbilityProjectileRP projectile = projectileObj.GetComponent<AbilityProjectileRP>();
        if (projectile != null)
        {
            projectile.damage = GetFinalDamage(ability.damage);
            projectile.enemyLayer = enemyLayer;
            projectile.breakableLayer = breakableLayer;

            Vector2 shootDirection = Vector2.right;

            if (transform.localScale.x < 0)
            {
                shootDirection = Vector2.left;
            }

            projectile.SetDirection(shootDirection);
        }
        else
        {
            Debug.LogWarning("Projectile prefab is missing AbilityProjectileRP on " + ability.abilityName);
        }
    }

    void UseFreezeAbility(AbilityDataRP ability)
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("AttackPoint is missing.");
            return;
        }

        SpawnEffectAtPoint(ability, attackPoint.position);

        int finalDamage = GetFinalDamage(ability.damage);

        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            enemyLayer
        );

        foreach (Collider2D hit in enemyHits)
        {
            EnemyHealthRP enemyHealth = hit.GetComponent<EnemyHealthRP>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(finalDamage);
            }

            EnemyStatusRP status = hit.GetComponent<EnemyStatusRP>();
            if (status != null)
            {
                status.Freeze(4f);
            }
        }

        Collider2D[] breakHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            breakableLayer
        );

        foreach (Collider2D hit in breakHits)
        {
            if (hit.CompareTag("breakitem"))
            {
                Destroy(hit.gameObject);
            }
        }
    }

    void UseCharmAbility(AbilityDataRP ability)
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("AttackPoint is missing.");
            return;
        }

        SpawnEffectAtPoint(ability, attackPoint.position);

        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            enemyLayer
        );

        foreach (Collider2D hit in enemyHits)
        {
            EnemyStatusRP status = hit.GetComponent<EnemyStatusRP>();
            if (status != null)
            {
                status.CharmRemove();
            }
            else
            {
                Destroy(hit.gameObject);
            }
        }

        Collider2D[] breakHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            breakableLayer
        );

        foreach (Collider2D hit in breakHits)
        {
            if (hit.CompareTag("breakitem"))
            {
                Destroy(hit.gameObject);
            }
        }
    }

    void UseShieldAuraAbility(AbilityDataRP ability)
    {
        StartCoroutine(WaterSwirlCoroutine(ability));
    }

    IEnumerator WaterSwirlCoroutine(AbilityDataRP ability)
    {
        float duration = 3f;
        float tickRate = 0.5f;
        float timer = 0f;

        GameObject swirlEffect = null;

        if (ability != null && ability.effectPrefab != null)
        {
            Vector3 spawnPosition = transform.position + (Vector3)effectOffset;
            swirlEffect = Instantiate(ability.effectPrefab, spawnPosition, Quaternion.identity);
            swirlEffect.transform.SetParent(transform);
        }

        if (playerDefense != null)
        {
            playerDefense.SetProtected(true);
        }

        while (timer < duration)
        {
            if (swirlEffect != null)
            {
                swirlEffect.transform.position = transform.position + (Vector3)effectOffset;
            }

            int finalDamage = GetFinalDamage(ability.damage);

            Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
                transform.position,
                ability.range,
                enemyLayer
            );

            foreach (Collider2D hit in enemyHits)
            {
                EnemyHealthRP enemy = hit.GetComponent<EnemyHealthRP>();
                if (enemy != null)
                {
                    enemy.TakeDamage(finalDamage);
                }
            }

            Collider2D[] breakHits = Physics2D.OverlapCircleAll(
                transform.position,
                ability.range,
                breakableLayer
            );

            foreach (Collider2D hit in breakHits)
            {
                if (hit.CompareTag("breakitem"))
                {
                    Destroy(hit.gameObject);
                }
            }

            timer += tickRate;
            yield return new WaitForSeconds(tickRate);
        }

        if (playerDefense != null)
        {
            playerDefense.SetProtected(false);
        }

        if (swirlEffect != null)
        {
            Destroy(swirlEffect);
        }
    }

    void SpawnEffectAtPoint(AbilityDataRP ability, Vector3 basePosition)
    {
        if (ability == null || ability.effectPrefab == null)
            return;

        Vector3 spawnPosition = basePosition + (Vector3)effectOffset;

        GameObject effect = Instantiate(
            ability.effectPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Destroy(effect, effectLifetime);
    }

    int GetFinalDamage(int baseDamage)
    {
        if (playerStats != null)
        {
            return playerStats.GetFinalDamage(baseDamage);
        }

        return baseDamage;
    }

    public void SelectAbility(int index)
    {
        if (abilities == null || index < 0 || index >= abilities.Length)
            return;

        if (abilities[index] == null)
        {
            Debug.LogWarning("Selected ability is null.");
            return;
        }

        if (!abilities[index].unlocked)
        {
            Debug.Log("That ability is still locked.");
            return;
        }

        currentAbilityIndex = index;
        Debug.Log("Selected ability: " + abilities[index].abilityName);
    }

    void OnDrawGizmosSelected()
    {
        if (abilities == null || abilities.Length == 0) return;
        if (currentAbilityIndex < 0 || currentAbilityIndex >= abilities.Length) return;
        if (abilities[currentAbilityIndex] == null) return;

        AbilityDataRP currentAbility = abilities[currentAbilityIndex];

        Gizmos.color = Color.red;

        if (currentAbility.type == AbilityType.Area || currentAbility.type == AbilityType.ShieldAura)
        {
            Gizmos.DrawWireSphere(transform.position, currentAbility.range);
        }
        else
        {
            if (attackPoint != null)
            {
                Gizmos.DrawWireSphere(attackPoint.position, currentAbility.range);
            }
        }
    }
}