using System.Collections;
using UnityEngine;

public class PlayerAttackRP : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint; // where attacks come from
    public LayerMask enemyLayer; // reconsing enemies
    public LayerMask breakableLayer; // reconsing breakable items 
    public PlayerStatsRP playerStats; // used for damage scaling
    public PlayerDefenseRP playerDefense; // for sheild defense
    public SpriteRenderer playerSprite; // flips ability animation

    [Header("Abilities")]
    public AbilityDataRP[] abilities = new AbilityDataRP[12]; // all abilities 
    public int currentAbilityIndex = 0; // starting abilies 

    [Header("Effect Settings")]
    public Vector2 effectOffset = Vector2.zero; // offsets visuals a bit
    public float effectLifetime = 0.5f; // how long the effect lasts 

    private float nextAttackTime = 0f; // cooldown tracker
    private bool isWaterSwirlActive = false; // prevents stacking abilitys 
    private string _sortingLayerName = "Default";
    private int _sortingOrder = 10;

    void Awake()
    {
        // Auto-detect sorting from the player's own SpriteRenderer
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
            TryUseCurrentAbility(); // press o to use ability
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
            Debug.Log("Ability is locked."); // can't use locked abilities
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

        // selects ability based on type
        switch (ability.type)
        {
            case AbilityType.Melee: // melee means close combat ability source of name taken from Playtank
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
        
        //hits enemies in a small circle in front of player
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            ability.range,
            enemyLayer
        );

        // breaks objects like vases
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
        
        // hits everything around player
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
        Vector3 scale = projectileObj.transform.localScale;
        scale.x = shootDirection == Vector2.left ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        projectileObj.transform.localScale = scale;

        AbilityProjectileRP projectile = projectileObj.GetComponent<AbilityProjectileRP>();
        if (projectile != null)
        {
            projectile.damage = GetFinalDamage(ability.damage);
            projectile.enemyLayer = enemyLayer;
            projectile.breakableLayer = breakableLayer;
            projectile.SetDirection(shootDirection);
        }
        else
        {
            Debug.LogWarning("Projectile prefab is missing AbilityProjectileRP on " + ability.abilityName);
            Destroy(projectileObj);
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
            // Final boss is intentionally unaffected by freeze abilities

            FinalBossHealthRP boss = hit.GetComponent<FinalBossHealthRP>();
            if (boss != null)
            {
                continue;
            }

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
            // Final boss is intentionally unaffected by charm abilities
            FinalBossHealthRP boss = hit.GetComponent<FinalBossHealthRP>();
            if (boss != null)
            {
                continue;
            }

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
        if (isWaterSwirlActive) return;
        StartCoroutine(WaterSwirlCoroutine(ability));
    }

    IEnumerator WaterSwirlCoroutine(AbilityDataRP ability)
    {
        isWaterSwirlActive = true;

        float duration = 3f;
        float tickRate = 0.5f;
        float timer = 0f;

        GameObject swirlEffect = null;

        if (ability != null && ability.effectPrefab != null)
        {
            Vector3 spawnPosition = transform.position + (Vector3)effectOffset;
            swirlEffect = Instantiate(ability.effectPrefab, spawnPosition, Quaternion.identity);
            ApplyEffectSortingOrder(swirlEffect);
            swirlEffect.transform.SetParent(transform);
        }

        if (playerDefense != null)
        {
            playerDefense.SetProtected(true); // player becomes protected
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
            playerDefense.SetProtected(false); // turns off shield
        }

        if (swirlEffect != null)
        {
            Destroy(swirlEffect);
        }

        isWaterSwirlActive = false;
    }

    void SpawnEffectAtPoint(AbilityDataRP ability, Vector3 basePosition)
    {
        if (ability == null || ability.effectPrefab == null) return;

        Vector3 spawnPosition = basePosition + (Vector3)effectOffset;

        GameObject effect = Instantiate(
            ability.effectPrefab,
            spawnPosition,
            Quaternion.identity
        );

        ApplyEffectSortingOrder(effect);
        Destroy(effect, effectLifetime); // removes visual after short time
    }

    void ApplyEffectSortingOrder(GameObject effect)
    {
        //makes effect render in front of player 
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
            return playerStats.GetFinalDamage(baseDamage);
        }

        return baseDamage;
    }

    public void SelectAbility(int index)
    {
        if (abilities == null || abilities.Length == 0) return;
        if (index < 0 || index >= abilities.Length) return;

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

        currentAbilityIndex = index; // switches ability 
        Debug.Log("Selected ability: " + abilities[index].abilityName);
    }

    void OnDrawGizmosSelected()
    {
        if (abilities == null || abilities.Length == 0) return;
        if (currentAbilityIndex < 0 || currentAbilityIndex >= abilities.Length) return;
        if (abilities[currentAbilityIndex] == null) return;

        AbilityDataRP currentAbility = abilities[currentAbilityIndex];

        Gizmos.color = Color.red;

        // shows attack range in editor 
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