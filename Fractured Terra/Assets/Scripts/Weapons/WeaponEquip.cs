using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public static WeaponEquip Instance;

    [Header("References")]
    public InventoryManager inventoryManager;
    public InventorySelect inventorySelect;
    public PlayerStatsRP playerStats;
    public SpriteRenderer weaponRenderer;

    [Header("Equipped Weapon")]
    public WeaponItem equippedWeapon;

    [Header("Defaults")]
    public int baseAttackPower = 0;
    public float baseAttackSpeed = 1.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            TryEquipWeapon();
        }
    }

    void TryEquipWeapon()
    {
        if (inventorySelect == null)
        {
            Debug.LogWarning("WeaponEquip: InventorySelect is missing.");
            return;
        }

        int index = inventorySelect.selectedIndex;

        if (index < 0 || index >= InventoryManager.items.Count)
        {
            Debug.Log("No item selected!");
            return;
        }

        InventoryItem item = InventoryManager.items[index];

        if (item is WeaponItem weapon)
        {
            equippedWeapon = weapon;
            ApplyWeaponStats(weapon);
            Debug.Log("Equipped: " + weapon.itemName);
        }
        else
        {
            Debug.Log("This item is not a weapon!");
        }
    }

    void ApplyWeaponStats(WeaponItem weapon)
    {
        if (playerStats != null)
        {
            playerStats.SetWeaponDamageBonus(weapon.attackPower);
        }

        if (weaponRenderer != null && weapon.attackSprite != null)
        {
            weaponRenderer.sprite = weapon.attackSprite;
        }
    }

    public int GetAttackPower()
    {
        return equippedWeapon != null ? equippedWeapon.attackPower : baseAttackPower;
    }

    public float GetAttackSpeed()
    {
        return equippedWeapon != null ? equippedWeapon.attackSpeed : baseAttackSpeed;
    }

    public void UnequipWeapon()
    {
        equippedWeapon = null;

        if (playerStats != null)
        {
            playerStats.SetWeaponDamageBonus(baseAttackPower);
        }

        if (weaponRenderer != null)
        {
            weaponRenderer.sprite = null;
        }

        Debug.Log("Weapon unequipped.");
    }
}