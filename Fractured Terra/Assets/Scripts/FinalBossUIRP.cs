using UnityEngine;
using UnityEngine.UI;

public class FinalBossUIRP : MonoBehaviour
{
    public GameObject bossBarRoot;
    public Image bossHealthFill;

    public void ShowBossBar()
    {
        if (bossBarRoot != null)
        {
            bossBarRoot.SetActive(true);
        }
    }

    public void HideBossBar()
    {
        if (bossBarRoot != null)
        {
            bossBarRoot.SetActive(false);
        }
    }

    public void UpdateBossBar(int currentHealth, int maxHealth)
    {
        if (bossHealthFill != null && maxHealth > 0)
        {
            bossHealthFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}