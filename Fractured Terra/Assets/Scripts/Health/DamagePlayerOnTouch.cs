using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}