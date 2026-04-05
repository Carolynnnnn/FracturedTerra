using UnityEngine;

public class EnemyHealthRP : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    private FinalBoss finalBoss;

    void Start()
    {
        currentHealth = maxHealth;
        finalBoss = GetComponent<FinalBoss>();
    }

    public void TakeDamage(int amount)
    {
        if (finalBoss != null)
        {
            finalBoss.TakeDamage(amount);
            return;
        }
        
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}