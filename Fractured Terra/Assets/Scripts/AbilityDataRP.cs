using UnityEngine;

[System.Serializable]
public class AbilityDataRP
{
    public string abilityName;

    [Header("Stats")]
    public float range = 1f;
    public int damage = 1;
    public float cooldown = 0f;

    [Header("Visuals")]
    public GameObject effectPrefab;

    [Header("Unlock")]
    public bool unlocked = false;

    [Header("Special Type")]
    public AbilityType type = AbilityType.Melee;
}

public enum AbilityType
{
    Melee,
    Projectile,
    Area,
    Freeze,
    Charm,
    ShieldAura
}