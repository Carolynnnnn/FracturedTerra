using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image fillImage;
    private FinalBoss boss;

    void Start()
    {
        boss = FindObjectOfType<FinalBoss>();
    }

    void Update()
    {
        if (boss != null && fillImage != null)
        {
            fillImage.fillAmount = (float)boss.currentHealth / boss.maxHealth;
        }
    }
}
