using UnityEngine;

public class Level2GemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gem") && other.enabled)
        {
            other.enabled = false; // Disables immediately so the second collider pair doesn't re-trigger
            GemManager.gemCount++;
            Destroy(other.gameObject);
        }
    }
}
