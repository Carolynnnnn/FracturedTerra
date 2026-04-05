using UnityEngine;

public class UnlockAbilityTriggerRP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (AbilityUnlockManagerRP.Instance != null)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(6);
        }

        Destroy(gameObject);
    }
}