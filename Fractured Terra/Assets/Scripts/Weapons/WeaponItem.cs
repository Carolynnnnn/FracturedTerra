using UnityEngine;

[System.Serializable]
public class WeaponItem : InventoryItem
{
    public int attackPower;
    public float attackSpeed;
    public Vector2 hitboxSize;
    public string acquiredInLevel;
    public Sprite attackSprite;

    public WeaponItem(string name, string desc, Sprite icon, int life, int atkPower, float atkSpeed, Vector2 hitbox, string level, Sprite atkSprite) : base(name, desc, icon, life, true, null)
    {
        attackPower = atkPower;
        attackSpeed = atkSpeed;
        hitboxSize = hitbox;
        acquiredInLevel = level;
        attackSprite = atkSprite;
    }
}
