using UnityEngine;

public class GoldKeyChestRP : MonoBehaviour, IInteractable
{
    public InventoryManager inventoryManager; // where the item gets added

    [Header("Gold Key Item Data")]
    public string itemName = "GoldKey"; // name of item added to inventory
    public string description = "A key that opens a locked border."; // shows in UI
    public Sprite icon; // icon for inventory
    public int maxLife = 1; // how many uses (just 1 for a key)
    public bool canUse = false; // key isn’t directly "used" like abilities
    public GameObject worldPrefab; // used if dropped into the world

    private Animator animator;
    private bool isAvailable = true; // makes sure chest can only be opened once

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool CanInteract()
    {
        return isAvailable; // only interact if chest hasn’t been opened yet
    }

    public void Interact()
    {
        if (!isAvailable) return;
        if (inventoryManager == null) return;

        // creates the key item and adds it to inventory
        InventoryItem newItem = new InventoryItem(itemName, description, icon, maxLife, canUse, worldPrefab);
        inventoryManager.AddItem(newItem);

        isAvailable = false; // prevents reopening

        if (animator != null)
        {
            animator.SetTrigger("OpenChest"); // plays chest opening animation
        }

        Debug.Log("Got GoldKey from chest"); // just for testing
    }

    public void RefillChest()
    {
        isAvailable = true; // lets chest be reused if needed (like reset level)
        Debug.Log("GoldKey chest refilled");
    }
}