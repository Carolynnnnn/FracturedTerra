using UnityEngine;

public class WaterPedestal : MonoBehaviour, IInteractable
{
    private bool collected = false; // Determines if the ability has been collected yet
    private Animator animator; // To stop animation
    
    void Start()
    {
        animator = GetComponent<Animator>(); // Gets animator from object
    }
    
    public bool CanInteract()
    {
        return !collected;
    }

    public void Interact()
    {
        if (collected) return; // Can only be collected once
        collected = true;
        AbilityUnlockManagerRP.Instance.UnlockAbility(11); // Unlock ability 12, water swirl
        animator.SetTrigger("Collected");
    }
}
