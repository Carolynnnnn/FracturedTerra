using UnityEngine;

public class UnlockAbility2RP : MonoBehaviour
{
    //same code as UnlockAbilityTriggerRP just with diffrent index 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (AbilityUnlockManagerRP.Instance != null)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(1); // unlocks Ability 2 index 1
        }

        Destroy(gameObject);
    }
}
