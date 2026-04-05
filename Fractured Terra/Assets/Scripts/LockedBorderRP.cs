using UnityEngine;

public class LockedBorderRP : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public string requiredItemName = "GoldKey";
    public Collider2D blockingCollider;
    public GoldKeyChestRP linkedChest;

    private bool unlocked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER HIT: " + other.name);

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Not player");
            return;
        }

        if (unlocked)
        {
            Debug.Log("Already unlocked");
            return;
        }

        if (inventoryManager == null)
        {
            Debug.Log("InventoryManager missing");
            return;
        }

        InventoryItem keyItem = inventoryManager.FindItemByName(requiredItemName);

        if (keyItem != null)
        {
            Debug.Log("KEY FOUND");
            UnlockBorder();
        }
        else
        {
            Debug.Log("NO KEY FOUND");
        }
    }

    private void UnlockBorder()
    {
        unlocked = true;

        Debug.Log("UNLOCKING BORDER");

        inventoryManager.RemoveItemByName(requiredItemName);

        if (blockingCollider != null)
        {
            Debug.Log("Disabling collider: " + blockingCollider.name);
            blockingCollider.enabled = false;
        }
        else
        {
            Debug.Log("blockingCollider is NULL");
        }

        if (linkedChest != null)
        {
            linkedChest.RefillChest();
        }
    }
}