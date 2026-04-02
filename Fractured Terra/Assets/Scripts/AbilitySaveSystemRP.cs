using UnityEngine;

public class AbilitySaveSystemRP : MonoBehaviour
{
    public PlayerAttackRP playerAttack;

    void Start()
    {
        LoadData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("All ability save data deleted.");
        }
    }

    public void SaveData()
    {
        if (playerAttack == null || playerAttack.abilities == null) return;

        for (int i = 0; i < playerAttack.abilities.Length; i++)
        {
            if (playerAttack.abilities[i] != null)
            {
                PlayerPrefs.SetInt("AbilityUnlocked_" + i,
                    playerAttack.abilities[i].unlocked ? 1 : 0);
            }
        }

        PlayerPrefs.SetInt("SelectedAbility", playerAttack.currentAbilityIndex);
        PlayerPrefs.Save();
        Debug.Log("Abilities saved.");
    }

    public void LoadData()
    {
        if (playerAttack == null || playerAttack.abilities == null) return;

        for (int i = 0; i < playerAttack.abilities.Length; i++)
        {
            if (playerAttack.abilities[i] != null)
            {
                int unlocked = PlayerPrefs.GetInt("AbilityUnlocked_" + i, -1);

                if (unlocked != -1)
                {
                    playerAttack.abilities[i].unlocked = (unlocked == 1);
                }
            }
        }

        int savedIndex = PlayerPrefs.GetInt("SelectedAbility", 0);

        if (savedIndex < 0 || savedIndex >= playerAttack.abilities.Length)
        {
            savedIndex = 0;
        }

        playerAttack.currentAbilityIndex = savedIndex;

        AbilityUIManagerRP ui = FindObjectOfType<AbilityUIManagerRP>();
        if (ui != null)
        {
            ui.RefreshAbilityUI();
        }

        Debug.Log("Abilities loaded.");
    }
}