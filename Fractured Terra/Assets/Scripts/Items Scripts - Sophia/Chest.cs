using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public InventoryManager inventoryManager; // Keeps track of player's inventory
    
    // Build item prize
    // Note: if name is gem, adds one to total instead
    public string itemName; // Item's name
    public string description; // Item's description
    public Sprite icon; // Item's sprite
    public int maxLife = 1; // Max amount of uses an item has, 1 use by default
    public bool canUse; // Determines if the item can be used
    public GameObject worldPrefab; // Helps drop item if necessary
                                   // note: event items SHOULD NOT HAVE THIS, as they should never be dropped
                                   
    // Other vars
    private Animator animator; // For opening animation
    private bool isOpened = false; // Determines if the chest has already been opened
    
    void Start()
    {
        animator = GetComponent<Animator>(); // Gets animator from object
    }

    public bool CanInteract()
    {
        return !isOpened;
    }
    public void Interact()
    {
        if (CanInteract()) OpenChest();
    }
    
    private void OpenChest()
    {
        isOpened = true;
        animator.SetTrigger("OpenChest"); // Plays chest opening animation
        
        // Give item to player
        if (itemName == "Gem") GemManager.gemCount++; // Gives a gem
        else
        { // Create a new inventory item and give it to player
            InventoryItem newItem = new InventoryItem(itemName, description, icon, maxLife, canUse, worldPrefab);
            inventoryManager.AddItem(newItem); // Adds item to inventory
        }

    }
    
}
