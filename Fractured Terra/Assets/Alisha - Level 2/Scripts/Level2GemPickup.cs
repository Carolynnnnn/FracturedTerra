using UnityEngine;

public class Level2GemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            GemManager.gemCount++;
        }
    }
}
