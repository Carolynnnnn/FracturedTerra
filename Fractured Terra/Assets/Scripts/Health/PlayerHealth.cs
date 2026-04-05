using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Regeneration")]
    public float regenRate = 2f; // health per second
    public float regenDelay = 4f; // wait this long after taking damage before regen starts

    [Header("UI")]
    public Image healthFillImage;

    private float lastDamageTime;
    private Vector3? checkpointPosition;

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        lastDamageTime = -regenDelay;
    }

    void Update()
    {
        HandleRegen();
        if (Input.GetKeyDown(KeyCode.H)) // TEMPORARLY ADD AS TEST REMOVE WHEN GAME STARST
        {
            TakeDamage(10f);
        }
    }

    void HandleRegen()
    {
        if (currentHealth < maxHealth && Time.time >= lastDamageTime + regenDelay)
        {
            currentHealth += regenRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            UpdateHealthBar();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        lastDamageTime = Time.time;
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Kill()
    {
        TakeDamage(currentHealth);
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player died");

        // Reset health
        currentHealth = maxHealth;
        UpdateHealthBar();
        
        // Respawn at checkpoint if one has been set, otherwise use the PlayerSpawner
        if (checkpointPosition.HasValue)
        {
            transform.position = checkpointPosition.Value;
        }
        else
        {
            PlayerSpawner playerSpawner = Object.FindFirstObjectByType<PlayerSpawner>();
            if (playerSpawner != null)
                transform.position = playerSpawner.transform.position;
            else
                Debug.LogWarning("PlayerSpawner not assigned!");
        }

    }
}