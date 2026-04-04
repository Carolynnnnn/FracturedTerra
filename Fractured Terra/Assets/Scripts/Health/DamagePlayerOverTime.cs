using UnityEngine;

public class DamagePlayerOverTime : MonoBehaviour
{
    public float damage = 10f;
    public float damageCooldown = 1f;

    private float lastDamageTime;

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        PlayerState state = other.GetComponent<PlayerState>();
        bool jumping = state != null && state.isJumping;

        if (playerHealth != null && !jumping && Time.time >= lastDamageTime + damageCooldown)
        {
            playerHealth.TakeDamage(damage);
            lastDamageTime = Time.time;
        }
    }
}