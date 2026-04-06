using UnityEngine;
using UnityEngine.UI;

public class FinalBossUIRP : MonoBehaviour
{
    public GameObject bossBarRoot; // the whole boss UI (so we can show/hide it)
    public Image bossHealthFill; // the fill part of the health bar

    public void ShowBossBar()
    {
        if (bossBarRoot != null)
        {
            bossBarRoot.SetActive(true); // turns boss UI on when fight starts
        }
    }

    public void HideBossBar()
    {
        if (bossBarRoot != null)
        {
            bossBarRoot.SetActive(false); // hides UI after boss dies
        }
    }

    public void UpdateBossBar(int currentHealth, int maxHealth)
    {
        if (bossHealthFill != null && maxHealth > 0)
        {
            // updates the fill amount (basically how full the health bar looks)
            bossHealthFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}