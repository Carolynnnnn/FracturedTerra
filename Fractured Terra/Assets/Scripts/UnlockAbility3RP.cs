using UnityEngine;

public class UnlockAbility3RP : MonoBehaviour
{
    //same code as UnlockAbilityTriggerRP just with diffrent index 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (AbilityUnlockManagerRP.Instance != null)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(2); // unlocks Ability 3 index 2
        }

        Destroy(gameObject);
    }
}
