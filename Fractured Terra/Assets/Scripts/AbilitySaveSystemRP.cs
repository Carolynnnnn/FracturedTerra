using UnityEngine;

public class AbilitySaveSystemRP : MonoBehaviour
{
    public PlayerAttackRP playerAttack; // reference to player so we can access abilities + current selection

    void Start()
    {
        LoadData(); // load saved abilities when the game starts
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            PlayerPrefs.DeleteAll(); // quick reset for testing (wipes all saved data)
            PlayerPrefs.Save();
            Debug.Log("All ability save data deleted.");
        }
    }

    public void SaveData()
    {
        if (playerAttack == null || playerAttack.abilities == null) return; // safety check

        for (int i = 0; i < playerAttack.abilities.Length; i++)
        {
            if (playerAttack.abilities[i] != null)
            {
                // saves whether each ability is unlocked (1 = true, 0 = false)
                PlayerPrefs.SetInt("AbilityUnlocked_" + i,
                    playerAttack.abilities[i].unlocked ? 1 : 0);
            }
        }

        PlayerPrefs.SetInt("SelectedAbility", playerAttack.currentAbilityIndex); // saves which ability is currently equipped
        PlayerPrefs.Save();
        Debug.Log("Abilities saved.");
    }

    public void LoadData()
    {
        if (playerAttack == null || playerAttack.abilities == null) return; // safety check

        for (int i = 0; i < playerAttack.abilities.Length; i++)
        {
            if (playerAttack.abilities[i] != null)
            {
                int unlocked = PlayerPrefs.GetInt("AbilityUnlocked_" + i, -1);

                if (unlocked != -1)
                {
                    playerAttack.abilities[i].unlocked = (unlocked == 1); // restores unlock state
                }
            }
        }

        int savedIndex = PlayerPrefs.GetInt("SelectedAbility", 0); // gets last equipped ability

        if (savedIndex < 0 || savedIndex >= playerAttack.abilities.Length)
        {
            savedIndex = 0; // fallback if something went wrong
        }

        playerAttack.currentAbilityIndex = savedIndex;

        AbilityUIManagerRP ui = FindObjectOfType<AbilityUIManagerRP>();
        if (ui != null)
        {
            ui.RefreshAbilityUI(); // updates UI so it matches loaded data
        }

        Debug.Log("Abilities loaded.");
    }
}