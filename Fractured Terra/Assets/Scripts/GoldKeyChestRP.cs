using UnityEngine;

public class GoldKeyChestRP : MonoBehaviour, IInteractable
{
    public InventoryManager inventoryManager;

    [Header("Gold Key Item Data")]
    public string itemName = "GoldKey";
    public string description = "A key that opens a locked border.";
    public Sprite icon;
    public int maxLife = 1;
    public bool canUse = false;
    public GameObject worldPrefab;

    private Animator animator;
    private bool isAvailable = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool CanInteract()
    {
        return isAvailable;
    }

    public void Interact()
    {
        if (!isAvailable) return;
        if (inventoryManager == null) return;

        InventoryItem newItem = new InventoryItem(itemName, description, icon, maxLife, canUse, worldPrefab);
        inventoryManager.AddItem(newItem);

        isAvailable = false;

        if (animator != null)
        {
            animator.SetTrigger("OpenChest");
        }

        Debug.Log("Got GoldKey from chest");
    }

    public void RefillChest()
    {
        isAvailable = true;
        Debug.Log("GoldKey chest refilled");
    }
}