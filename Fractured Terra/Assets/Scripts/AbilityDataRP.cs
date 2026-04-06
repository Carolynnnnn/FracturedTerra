using UnityEngine;

[System.Serializable] // lets this show up in the inspector (so I can tweak abilities in Unity easily)
public class AbilityDataRP
{
    public string abilityName; // just the name of the ability (for UI / debugging)

    [Header("Stats")]
    public float range = 1f; // how far the ability can reach (like melee distance or projectile travel)
    public int damage = 1; // how much damage it does to enemies
    public float cooldown = 0f; // time before you can use it again

    [Header("Visuals")]
    public GameObject effectPrefab; // the visual effect (like slash, fireball, etc)

    [Header("Unlock")]
    public bool unlocked = false; // if false, player can’t use it yet (used for progression)

    [Header("Special Type")]
    public AbilityType type = AbilityType.Melee; // determines how the ability behaves (melee, projectile, etc)
}

// different types of abilities in my game (used to change logic depending on type)
public enum AbilityType
{
    Melee,        // close range attack (ex. sword slash)
    Projectile,   // shoots something forward (ex. magic)
    Area,         // hits an area around player
    Freeze,       // freezes enemies
    Charm,        // makes enemies friendly / confused
    ShieldAura    // defensive ability around player
}