using UnityEngine;

public class AbilityUnlockManagerRP : MonoBehaviour
{
    public static AbilityUnlockManagerRP Instance; // singleton so I can call this from anywhere (like pickups)

    [Header("References")]
    public PlayerAttackRP playerAttack; // holds all abilities
    public AbilityUIManagerRP abilityUIManager; // updates UI when something unlocks

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // set global reference
        }
        else
        {
            Destroy(gameObject); // prevents duplicates
        }
    }

    public void UnlockAbility(int index)
    {
        if (playerAttack == null) return;
        if (playerAttack.abilities == null) return;
        if (index < 0 || index >= playerAttack.abilities.Length) return;
        if (playerAttack.abilities[index] == null) return;

        if (!playerAttack.abilities[index].unlocked)
        {
            playerAttack.abilities[index].unlocked = true; // actually unlocks the ability (used when player picks something up)

            AbilitySaveSystemRP save = FindObjectOfType<AbilitySaveSystemRP>();
            if (save != null)
            {
                save.SaveData(); // saves right away so progress isn’t lost
            }

            Debug.Log("Unlocked ability: " + playerAttack.abilities[index].abilityName);
        }

        if (abilityUIManager != null)
        {
            abilityUIManager.RefreshAbilityUI(); // updates buttons + visuals instantly
        }
    }

    public bool IsAbilityUnlocked(int index)
    {
        if (playerAttack == null) return false;
        if (playerAttack.abilities == null) return false;
        if (index < 0 || index >= playerAttack.abilities.Length) return false;
        if (playerAttack.abilities[index] == null) return false;

        return playerAttack.abilities[index].unlocked; // lets other scripts check if player has an ability
    }
}