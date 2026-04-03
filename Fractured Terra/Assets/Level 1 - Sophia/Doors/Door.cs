using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public InventoryManager inventoryManager; // Keeps track of player's inventory

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        bool hadItem = inventoryManager.RemoveItemByName("Key"); // Uses the key item
        if (hadItem) Destroy(gameObject); // Destroys the door (if a key was used)
    }
}
