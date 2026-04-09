using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIManagerRP : MonoBehaviour
{
    [Header("References")]
    public PlayerAttackRP playerAttack; // gives access to abilities + current selection
    public Button[] abilityButtons; // buttons the player clicks to select abilities
    public Image[] abilityImages; // icons for each ability
    public TMP_Text descriptionText; // text box that shows stats/info

    [Header("Locked Look")]
    public float lockedAlpha = 0.35f; // makes locked abilities look faded
    public float unlockedAlpha = 1f; // normal look when unlocked

    void Start()
    {
        RefreshAbilityUI(); // sets everything up at the start
    }

    public void RefreshAbilityUI()
    {
        if (playerAttack == null || playerAttack.abilities == null) return;

        for (int i = 0; i < abilityButtons.Length && i < playerAttack.abilities.Length; i++)
        {
            int index = i; // needed so the button remembers the right ability
            AbilityDataRP ability = playerAttack.abilities[i];

            if (abilityButtons[i] != null)
            {
                abilityButtons[i].onClick.RemoveAllListeners(); // clears old listeners so no duplicates
                abilityButtons[i].onClick.AddListener(() => OnAbilityClicked(index)); // assigns click action
                abilityButtons[i].interactable = ability.unlocked; // can only click if unlocked
            }

            if (abilityImages != null && i < abilityImages.Length && abilityImages[i] != null)
            {
                Color c = abilityImages[i].color;
                c.a = ability.unlocked ? unlockedAlpha : lockedAlpha; // fades locked abilities
                abilityImages[i].color = c;
            }
        }

        UpdateDescription(playerAttack.currentAbilityIndex); // updates info panel
    }

    public void OnAbilityClicked(int index)
    {
        if (playerAttack == null) return;
        if (index < 0 || index >= playerAttack.abilities.Length) return;

        if (!playerAttack.abilities[index].unlocked)
        {
            Debug.Log("Ability locked."); // just in case somehow clicked
            return;
        }

        playerAttack.SelectAbility(index); // equips the ability
        UpdateDescription(index); // updates the UI text
    }

    public void UpdateDescription(int index)
    {
        if (descriptionText == null || playerAttack == null) return;
        if (index < 0 || index >= playerAttack.abilities.Length) return;

        AbilityDataRP ability = playerAttack.abilities[index];
        if (ability == null) return;

        string status = ability.unlocked ? "Unlocked" : "Locked";

        // builds the little info panel (name + stats)
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

        playerAttack.abilities[index].unlocked = true; // unlocks ability (used for pickups / progression)
        RefreshAbilityUI(); // refresh so it updates visually right away
    }
}