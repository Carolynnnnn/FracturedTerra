using UnityEngine;

public class UnlockAbility11RP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return; // only player can trigger this

        if (AbilityUnlockManagerRP.Instance != null)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(10); // unlocks ability at index 10 
        }

        Destroy(gameObject); // removes the trigger after use (like a one-time collectible)
    }
}
