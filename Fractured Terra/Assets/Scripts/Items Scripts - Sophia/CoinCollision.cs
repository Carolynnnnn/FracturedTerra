using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    // Object initialization
    public CoinManager cm; // Stores coin count

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin")) // When player collides with a coin 
        {
            Destroy(other.gameObject); // Coin disappears
            cm.coinCount++; // Increases coin count
        }
    }
}
