using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public static WeaponItem WoodSword = new WeaponItem(
        "Wood Sword", "A basic wooden sword.",
        null, 999, 5, 1.0f, new Vector2(1f, 1f), "HubWorld", null);

    public static WeaponItem StoneSword = new WeaponItem(
        "Stone Sword", "A sturdy stone sword.",
        null, 999, 10, 0.9f, new Vector2(1.2f, 1f), "Jungle", null);

    public static WeaponItem FireSword = new WeaponItem(
        "Fire Sword", "Burns with intense heat.",
        null, 999, 15, 0.8f, new Vector2(1.3f, 1f), "Fire", null);

    public static WeaponItem DarkSword = new WeaponItem(
        "Dark Sword", "Corrupted by unknown energy.",
        null, 999, 20, 0.7f, new Vector2(1.5f, 1f), "Corrupted", null);

    public static WeaponItem LightSword = new WeaponItem(
        "Light Sword", "Enlightened by the holy light.",
        null, 999, 18, 1.2f, new Vector2(1.3f, 1.2f), "Light", null);

    public static WeaponItem Trident = new WeaponItem(
        "Trident", "Blessed by the purest water.",
        null, 999, 12, 1.1f, new Vector2(1.4f, 1.5f), "Water", null);
}