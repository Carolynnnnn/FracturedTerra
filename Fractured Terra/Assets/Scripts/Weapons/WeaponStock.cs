using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStock", menuName = "Shop Stock/Weapon")]
public class WeaponStock : ShopStock
{
    [Header("Weapon Stats")]
    public int attackPower = 5;
    public float attackSpeed = 1.0f;
    public Vector2 hitboxSize = new Vector2(1f, 1f);
    public string acquiredInLevel = "HubWorld";
    public Sprite attackSprite;
}