using UnityEngine;

public class AbilitySaves : MonoBehaviour
{
    public void Start()
    {
        if (GemManager.gemCount == 1)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(1); // unlocks Ability 6 index 5
        }
        else if (GemManager.gemCount == 2) 
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(1); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(11); 
        }
        else if (GemManager.gemCount == 3)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(1); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(11); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(10); 
        }
        else if (GemManager.gemCount == 4)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(1); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(11); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(10); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(2); 
        }
        else if (GemManager.gemCount == 5)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(1); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(11); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(10); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(2);
            AbilityUnlockManagerRP.Instance.UnlockAbility(6);
        }
        else if (GemManager.gemCount == 6)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(1); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(11); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(10); 
            AbilityUnlockManagerRP.Instance.UnlockAbility(2);
            AbilityUnlockManagerRP.Instance.UnlockAbility(6);
            AbilityUnlockManagerRP.Instance.UnlockAbility(5);
        }
    }
}
