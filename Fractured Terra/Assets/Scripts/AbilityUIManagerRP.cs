using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIManagerRP : MonoBehaviour
{
    [Header("References")]
    public PlayerAttackRP playerAttack;
    public Button[] abilityButtons;
    public Image[] abilityImages;
    public TMP_Text descriptionText;

    [Header("Locked Look")]
    public float lockedAlpha = 0.35f;
    public float unlockedAlpha = 1f;

    void Start()
    {
        RefreshAbilityUI();
    }

    public void RefreshAbilityUI()
    {
        if (playerAttack == null || playerAttack.abilities == null) return;

        for (int i = 0; i < abilityButtons.Length && i < playerAttack.abilities.Length; i++)
        {
            int index = i;
            AbilityDataRP ability = playerAttack.abilities[i];

            if (abilityButtons[i] != null)
            {
                abilityButtons[i].onClick.RemoveAllListeners();
                abilityButtons[i].onClick.AddListener(() => OnAbilityClicked(index));
                abilityButtons[i].interactable = ability.unlocked;
            }

            if (abilityImages != null && i < abilityImages.Length && abilityImages[i] != null)
            {
                Color c = abilityImages[i].color;
                c.a = ability.unlocked ? unlockedAlpha : lockedAlpha;
                abilityImages[i].color = c;
            }
        }

        UpdateDescription(playerAttack.currentAbilityIndex);
    }

    public void OnAbilityClicked(int index)
    {
        if (playerAttack == null) return;
        if (index < 0 || index >= playerAttack.abilities.Length) return;

        if (!playerAttack.abilities[index].unlocked)
        {
            Debug.Log("Ability locked.");
            return;
        }

        playerAttack.SelectAbility(index);
        UpdateDescription(index);
    }

    public void UpdateDescription(int index)
    {
        if (descriptionText == null || playerAttack == null) return;
        if (index < 0 || index >= playerAttack.abilities.Length) return;

        AbilityDataRP ability = playerAttack.abilities[index];
        if (ability == null) return;

        string status = ability.unlocked ? "Unlocked" : "Locked";

        descriptionText.text =
            ability.abilityName + "\n" +
            "Damage: " + ability.damage + "\n" +
            "Range: " + ability.range + "\n" +
            "Cooldown: " + ability.cooldown + "s\n" +
            status;
    }

    public void UnlockAbility(int index)
    {
        if (playerAttack == null) return;
        if (index < 0 || index >= playerAttack.abilities.Length) return;

        playerAttack.abilities[index].unlocked = true;
        RefreshAbilityUI();
    }
}