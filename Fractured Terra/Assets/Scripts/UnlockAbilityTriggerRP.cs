using UnityEngine;

public class UnlockAbilityTriggerRP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return; // only player can trigger this

        if (AbilityUnlockManagerRP.Instance != null)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(6); // unlocks ability at index 6 (pickup reward)
        }

        Destroy(gameObject); // removes the trigger after use (like a one-time collectible)
    }
}