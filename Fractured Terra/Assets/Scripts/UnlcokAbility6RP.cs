using UnityEngine;

public class UnlcokAbility6RP : MonoBehaviour
{
    //same code as UnlockAbilityTriggerRP just with diffrent index 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (AbilityUnlockManagerRP.Instance != null)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(5); // unlocks Ability 6 index 5
        }

        Destroy(gameObject);
    }
}
