using UnityEngine;

// Attach this to the Player in Japneet_Level3.
// Deals damage when the player collides with objects (walls, obstacles, etc.).
// Ground collisions are ignored so normal landing does not hurt the player.
public class CollisionDamage : MonoBehaviour
{
    [Header("Damage")]
    [Tooltip("How much health is lost per collision hit.")]
    public float damageAmount = 5f;

    [Tooltip("Minimum seconds between damage hits (prevents rapid spam).")]
    public float damageCooldown = 0.5f;

    [Tooltip("Tags to ignore (e.g. Ground so landing doesn't hurt).")]
    public string[] ignoreTags = { "Ground" };

    private PlayerHealth playerHealth;
    private float lastHitTime = -999f;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Skip ignored tags
        foreach (string tag in ignoreTags)
        {
            if (collision.gameObject.CompareTag(tag))
                return;
        }

        // Enforce cooldown
        if (Time.time - lastHitTime < damageCooldown)
            return;

        lastHitTime = Time.time;
        playerHealth?.TakeDamage(damageAmount);
    }
}
