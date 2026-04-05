using UnityEngine;

public class FinalBossHealthRP : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    public FinalBossUIRP bossUI;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (bossUI != null)
        {
            bossUI.ShowBossBar();
            bossUI.UpdateBossBar(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (animator != null)
        {
            animator.SetTrigger("TakeHit");
        }

        if (bossUI != null)
        {
            bossUI.UpdateBossBar(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (bossUI != null)
        {
            bossUI.HideBossBar();
        }

        Destroy(gameObject);
    }
}