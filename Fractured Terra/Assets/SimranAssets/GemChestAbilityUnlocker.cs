using UnityEngine;

// Attached to the GemChest instance in Simran_Level5 to unlock ability 5
// when the chest is collected. Does not modify the shared GemChest prefab.
public class GemChestAbilityUnlocker : MonoBehaviour
{
    private Chest chest;
    private bool triggered = false;

    void Start()
    {
        chest = GetComponent<Chest>();
    }

    void Update()
    {
        if (!triggered && chest != null && !chest.CanInteract())
        {
            triggered = true;
            if (AbilityUnlockManagerRP.Instance != null)
                AbilityUnlockManagerRP.Instance.UnlockAbility(5);
        }
    }
}
