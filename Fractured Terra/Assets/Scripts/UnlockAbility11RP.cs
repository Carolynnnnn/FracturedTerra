using UnityEngine;

public class UnlockAbility11RP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (AbilityUnlockManagerRP.Instance != null)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(10); // unlocks Ability 11 index 10 
        }

        Destroy(gameObject);
    }
}
