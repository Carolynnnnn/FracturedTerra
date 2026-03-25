using UnityEngine;

public class DamagePlayerOverTime : MonoBehaviour
{
    public float damage = 10f;
    public float damageCooldown = 1f;

    private float lastDamageTime;

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null && Time.time >= lastDamageTime + damageCooldown)
        {
            playerHealth.TakeDamage(damage);
            lastDamageTime = Time.time;
        }
    }
}