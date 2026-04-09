using UnityEngine;

public class FinalBossHealthRP : MonoBehaviour
{
    public int maxHealth = 50; // total boss health
    public int currentHealth;

    public FinalBossUIRP bossUI; // UI for the boss health bar
    private Animator animator;
    private bool isDead = false; // prevents extra damage after death

    void Start()
    {
        currentHealth = maxHealth; // sets starting health
        animator = GetComponent<Animator>();

        if (bossUI != null)
        {
            bossUI.ShowBossBar(); // shows boss bar when fight starts
            bossUI.UpdateBossBar(currentHealth, maxHealth); // sets initial value
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // stops taking damage after death

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // keeps health in valid range

        if (animator != null)
        {
            animator.SetTrigger("TakeHit"); // plays hit animation
        }

        if (bossUI != null)
        {
            bossUI.UpdateBossBar(currentHealth, maxHealth); // updates boss health bar
        }

        if (currentHealth <= 0)
        {
            Die(); // kills boss when health hits 0
        }
    }

    void Die()
    {
        isDead = true;

        if (bossUI != null)
        {
            bossUI.HideBossBar(); // hides UI when boss dies
        }

        Destroy(gameObject); // removes boss from scene
    }
}