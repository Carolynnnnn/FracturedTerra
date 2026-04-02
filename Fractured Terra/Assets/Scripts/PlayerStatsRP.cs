using UnityEngine;

public class PlayerStatsRP : MonoBehaviour
{
    [Header("Damage Bonus From Weapon")]
    public int weaponDamageBonus = 0;

    public int GetFinalDamage(int baseDamage)
    {
        return baseDamage + weaponDamageBonus;
    }

    public void SetWeaponDamageBonus(int bonus)
    {
        weaponDamageBonus = bonus;
    }
}