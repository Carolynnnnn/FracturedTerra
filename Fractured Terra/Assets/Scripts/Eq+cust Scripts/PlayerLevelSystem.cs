using UnityEngine;
using TMPro;

public class PlayerLevelSystem : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text levelText;

    [Header("Progression")]
    public int currentLevel = 1;

    private void Start()
    {
        UpdateLevelUI();
    }

    public void SetLevel(int newLevel)
    {
        currentLevel = newLevel;
        UpdateLevelUI();
    }

    public void AddLevel(int amount)
    {
        currentLevel += amount;

        if (currentLevel < 1)
            currentLevel = 1;

        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        if (levelText != null)
        {
            levelText.text = "Lv. " + currentLevel;
        }
    }
}
