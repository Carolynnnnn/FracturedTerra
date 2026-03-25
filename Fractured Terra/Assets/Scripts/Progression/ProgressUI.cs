using UnityEngine;
using TMPro;

public class ProgressUI : MonoBehaviour
{
    public PlayerProgress playerProgress;

    public TMP_Text coinsText;
    public TMP_Text xpText;
    public TMP_Text armorText;

    void Update()
    {
        if (playerProgress == null) return;

        coinsText.text = "Coins: " + playerProgress.coins;
        xpText.text = "XP: " + playerProgress.xp;
        armorText.text = "Armor: " + playerProgress.armor;
    }
}