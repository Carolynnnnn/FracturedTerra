using System.Collections;
using UnityEngine;

public class PlayerAttackRP : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint; // where attacks come from
    public LayerMask enemyLayer; // what counts as enemy
    public LayerMask breakableLayer; // things like vases
    public PlayerStatsRP playerStats; // used for damage scaling
    public PlayerDefenseRP playerDefense; // used for shield ability
    public SpriteRenderer playerSprite; // used to detect direction (flip)

    [Header("Abilities")]
    public AbilityDataRP[] abilities = new AbilityDataRP[12]; // all abilities player can have
    public int currentAbilityIndex = 0; // which one is currently selected

    [Header("Effect Settings")]
    public Vector2 effectOffset = Vector2.zero; // offsets visuals a bit
    public float effectLifetime = 0.5f; // how long effects stay

    private float nextAttackTime = 0f; // cooldown tracker
    private bool isWaterSwirlActive = false; // prevents stacking shield ability
    private string _sortingLayerName = "Default";
    private int _sortingOrder = 10;

    void Awake()
    {
        // grabs player sprite sorting so effects appear in front
        SpriteRenderer sr = playerSprite != null
            ? playerSprite
            : GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
        {
            _sortingLayerName = sr.sortingLayerName;
            _sortingOrder = sr.sortingOrder + 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TryUseCurrentAbility(); // press O to use ability
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
            Debug.Log("Ability is locked."); // can’t use locked abilities
            return;
        }

        if (Time.time < nextAttackTime)
        {
            return; // still on cooldown
        }

        UseAbility(currentAbility);
        nextAttackTime = Time.time + currentAbility.cooldown;
    }

    void UseAbility(AbilityDataRP ability)
    {
        if (ability == null) return;

        // picks behavior based on type
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

        // hits enemies in a small circle in front of player
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            enemyLayer
        );

        foreach (Collider2D hit in enemyHits)
        {
            FinalBossHealthRP boss = hit.GetComponent<FinalBossHealthRP>();
            if (boss != null)
            {
                boss.TakeDamage(finalDamage);
            }
            else
            {
                EnemyHealthRP enemy = hit.GetComponent<EnemyHealthRP>();
                if (enemy != null)
                {
                    enemy.TakeDamage(finalDamage);
                }
            }
        }

        // breaks objects like vases
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

        // hits everything around player
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            transform.position,
            ability.range,
            enemyLayer
        );

        foreach (Collider2D hit in enemyHits)
        {
            FinalBossHealthRP boss = hit.GetComponent<FinalBossHealthRP>();
            if (boss != null)
            {
                boss.TakeDamage(finalDamage);
            }
            else
            {
                EnemyHealthRP enemy = hit.GetComponent<EnemyHealthRP>();
                if (enemy != null)
                {
                    enemy.TakeDamage(finalDamage);
                }
            }
        }
    }

    void UseProjectileAbility(AbilityDataRP ability)
    {
        if (attackPoint == null) return;
        if (ability.effectPrefab == null) return;

        // decides shoot direction based on player facing
        Vector2 shootDirection = Vector2.right;

        if (playerSprite != null && playerSprite.flipX)
        {
            shootDirection = Vector2.left;
        }

        GameObject projectileObj = Instantiate(
            ability.effectPrefab,
            attackPoint.position + (Vector3)effectOffset,
            Quaternion.identity
        );

        ApplyEffectSortingOrder(projectileObj);

        AbilityProjectileRP projectile = projectileObj.GetComponent<AbilityProjectileRP>();
        if (projectile != null)
        {
            projectile.damage = GetFinalDamage(ability.damage);
            projectile.enemyLayer = enemyLayer;
            projectile.breakableLayer = breakableLayer;
            projectile.SetDirection(shootDirection);
        }
    }

    void UseFreezeAbility(AbilityDataRP ability)
    {
        SpawnEffectAtPoint(ability, attackPoint.position);

        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            enemyLayer
        );

        foreach (Collider2D hit in enemyHits)
        {
            // boss is immune to freeze
            if (hit.GetComponent<FinalBossHealthRP>() != null) continue;

            EnemyStatusRP status = hit.GetComponent<EnemyStatusRP>();
            if (status != null)
            {
                status.Freeze(4f); // freezes enemy
            }
        }
    }

    void UseCharmAbility(AbilityDataRP ability)
    {
        SpawnEffectAtPoint(ability, attackPoint.position);

        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            enemyLayer
        );

        foreach (Collider2D hit in enemyHits)
        {
            // boss is immune to charm
            if (hit.GetComponent<FinalBossHealthRP>() != null) continue;

            EnemyStatusRP status = hit.GetComponent<EnemyStatusRP>();
            if (status != null)
            {
                status.CharmRemove(); // removes enemy instantly
            }
        }
    }

    void UseShieldAuraAbility(AbilityDataRP ability)
    {
        if (isWaterSwirlActive) return;
        StartCoroutine(WaterSwirlCoroutine(ability)); // starts shield effect over time
    }

    IEnumerator WaterSwirlCoroutine(AbilityDataRP ability)
    {
        isWaterSwirlActive = true;

        float duration = 3f;
        float tickRate = 0.5f;
        float timer = 0f;

        if (playerDefense != null)
        {
            playerDefense.SetProtected(true); // player becomes protected
        }

        while (timer < duration)
        {
            // deals damage repeatedly around player
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
                    enemy.TakeDamage(GetFinalDamage(ability.damage));
                }
            }

            timer += tickRate;
            yield return new WaitForSeconds(tickRate);
        }

        if (playerDefense != null)
        {
            playerDefense.SetProtected(false); // turns off shield
        }

        isWaterSwirlActive = false;
    }

    void SpawnEffectAtPoint(AbilityDataRP ability, Vector3 basePosition)
    {
        if (ability == null || ability.effectPrefab == null) return;

        GameObject effect = Instantiate(
            ability.effectPrefab,
            basePosition + (Vector3)effectOffset,
            Quaternion.identity
        );

        ApplyEffectSortingOrder(effect);
        Destroy(effect, effectLifetime); // removes visual after short time
    }

    void ApplyEffectSortingOrder(GameObject effect)
    {
        // makes effects render in front of player
        foreach (SpriteRenderer sr in effect.GetComponentsInChildren<SpriteRenderer>(true))
        {
            sr.sortingLayerName = _sortingLayerName;
            sr.sortingOrder = _sortingOrder;
        }
    }

    int GetFinalDamage(int baseDamage)
    {
        if (playerStats != null)
        {
            return playerStats.GetFinalDamage(baseDamage); // applies player upgrades
        }

        return baseDamage;
    }

    public void SelectAbility(int index)
    {
        if (abilities == null || abilities.Length == 0) return;
        if (index < 0 || index >= abilities.Length) return;

        if (!abilities[index].unlocked)
        {
            Debug.Log("That ability is still locked.");
            return;
        }

        currentAbilityIndex = index; // switches ability
        Debug.Log("Selected ability: " + abilities[index].abilityName);
    }

    void OnDrawGizmosSelected()
    {
        if (abilities == null || abilities.Length == 0) return;

        AbilityDataRP currentAbility = abilities[currentAbilityIndex];

        Gizmos.color = Color.red;

        // shows attack range in editor (super helpful for debugging)
        if (currentAbility.type == AbilityType.Area || currentAbility.type == AbilityType.ShieldAura)
        {
            Gizmos.DrawWireSphere(transform.position, currentAbility.range);
        }
        else if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, currentAbility.range);
        }
    }
}