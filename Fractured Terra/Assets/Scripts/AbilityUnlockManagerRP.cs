using UnityEngine;

public class AbilityUnlockManagerRP : MonoBehaviour
{
    public static AbilityUnlockManagerRP Instance;

    [Header("References")]
    public PlayerAttackRP playerAttack;
    public AbilityUIManagerRP abilityUIManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
            playerAttack.abilities[index].unlocked = true;
            
            AbilitySaveSystemRP save = FindObjectOfType<AbilitySaveSystemRP>();
            if (save != null)
            {
                save.SaveData();
            }
            
            Debug.Log("Unlocked ability: " + playerAttack.abilities[index].abilityName);
        }

        if (abilityUIManager != null)
        {
            abilityUIManager.RefreshAbilityUI();
        }
    }

    public bool IsAbilityUnlocked(int index)
    {
        if (playerAttack == null) return false;
        if (playerAttack.abilities == null) return false;
        if (index < 0 || index >= playerAttack.abilities.Length) return false;
        if (playerAttack.abilities[index] == null) return false;

        return playerAttack.abilities[index].unlocked;
    }
}