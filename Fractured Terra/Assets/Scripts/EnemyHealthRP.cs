using UnityEngine;

public class EnemyHealthRP : MonoBehaviour
{
    public int maxHealth = 25; // starting health for the enemy
    private int currentHealth;
    private FinalBoss finalBoss; // checks if this enemy is actually the final boss

    void Start()
    {
        currentHealth = maxHealth; // sets health at start
        finalBoss = GetComponent<FinalBoss>(); // if this exists, we treat it differently
    }

    public void TakeDamage(int amount)
    {
        if (finalBoss != null)
        {
            finalBoss.TakeDamage(amount); // sends damage to boss script instead (boss has its own system)
            return;
        }
        
        currentHealth -= amount; // normal enemy takes damage

        if (currentHealth <= 0)
        {
            Die(); // destroy enemy when health hits 0
        }
    }

    void Die()
    {
        Destroy(gameObject); // removes enemy from scene (used for regular enemies)
    }
}