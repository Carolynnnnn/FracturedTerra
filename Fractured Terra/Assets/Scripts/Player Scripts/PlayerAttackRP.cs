using UnityEngine;

public class PlayerAttackRP : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public LayerMask breakableLayer;
    public PlayerStatsRP playerStats;

    [Header("Abilities")]
    public AbilityDataRP[] abilities = new AbilityDataRP[12];
    public int currentAbilityIndex = 0;

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
        if (abilities == null || abilities.Length == 0) return;
        if (currentAbilityIndex < 0 || currentAbilityIndex >= abilities.Length) return;

        AbilityDataRP currentAbility = abilities[currentAbilityIndex];

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
        }
    }

    void UseMeleeAbility(AbilityDataRP ability)
    {
        SpawnEffect(ability);

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
        SpawnEffect(ability);

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
        SpawnEffect(ability);
        Debug.Log("Projectile ability used: " + ability.abilityName);
    }

    void UseFreezeAbility(AbilityDataRP ability)
    {
        SpawnEffect(ability);
        Debug.Log("Freeze ability used: " + ability.abilityName);
    }

    void UseCharmAbility(AbilityDataRP ability)
    {
        SpawnEffect(ability);
        Debug.Log("Charm ability used: " + ability.abilityName);
    }

    void UseShieldAuraAbility(AbilityDataRP ability)
    {
        SpawnEffect(ability);
        Debug.Log("Shield aura ability used: " + ability.abilityName);
    }

    void SpawnEffect(AbilityDataRP ability)
    {
        if (ability.effectPrefab != null && attackPoint != null)
        {
            GameObject effect = Instantiate(
                ability.effectPrefab,
                attackPoint.position,
                Quaternion.identity
            );

            Destroy(effect, 0.5f);
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
        if (index < 0 || index >= abilities.Length) return;

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

        AbilityDataRP currentAbility = abilities[currentAbilityIndex];

        Gizmos.color = Color.red;

        if (currentAbility.type == AbilityType.Area)
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