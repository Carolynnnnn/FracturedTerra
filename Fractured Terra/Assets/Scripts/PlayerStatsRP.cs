using UnityEngine;

public class PlayerStatsRP : MonoBehaviour
{
    [Header("Damage Bonus From Weapon")]
    public int weaponDamageBonus = 0; // extra damage from upgrades / weapons

    public int GetFinalDamage(int baseDamage)
    {
        return baseDamage + weaponDamageBonus; // adds bonus to ability damage
    }

    public void SetWeaponDamageBonus(int bonus)
    {
        weaponDamageBonus = bonus; // used when player equips something stronger
    }
}