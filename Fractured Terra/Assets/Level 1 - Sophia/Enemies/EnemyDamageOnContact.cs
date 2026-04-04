using UnityEngine;

public class EnemyDamageOnContact : MonoBehaviour
{
    public int damage = 1; // Damage enemy deals on contact with player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>(); // Gets player health

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Damages player
            }
        }
    }
}
