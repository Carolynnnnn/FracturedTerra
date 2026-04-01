using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public static WeaponEquip Instance;
    public InventoryManager inventoryManager;
    public InventorySelect inventorySelect;
    public PlayerAttackRP playerAttack;
    public AttackHitbox attackHitbox;
    public SpriteRenderer weaponRenderer;
    public WeaponItem equippedWeapon;
    public int baseAttackPower = 5;
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
        if (playerAttack != null)
            playerAttack.attackRange = weapon.hitboxSize.x;

        if (attackHitbox != null)
        {
            var dmgField = typeof(AttackHitbox).GetField("damage",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);
            if (dmgField != null)
                dmgField.SetValue(attackHitbox, weapon.attackPower);
        }

        if (weaponRenderer != null && weapon.attackSprite != null)
            weaponRenderer.sprite = weapon.attackSprite;
    }
    public int GetAttackPower()
    {
        return equippedWeapon != null ? equippedWeapon.attackPower : baseAttackPower;
    }
    public float GetAttackSpeed()
    {
        return equippedWeapon != null ? equippedWeapon.attackSpeed : baseAttackSpeed;
    }
}