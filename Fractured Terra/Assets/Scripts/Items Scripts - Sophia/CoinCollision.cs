using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin")) // When player collides with a coin 
        {
            Destroy(other.gameObject); // Coin disappears
            CoinManager.coinCount++; // Increases coin count
        }
    }
}
