using UnityEngine;

public class LockedBorderRP : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public string requiredItemName = "GoldKey";
    public Collider2D[] blockingColliders;
    public GoldKeyChestRP linkedChest;

    private bool unlocked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (unlocked) return;
        if (inventoryManager == null) return;

        InventoryItem keyItem = inventoryManager.FindItemByName(requiredItemName);

        if (keyItem != null)
        {
            UnlockBorder();
        }
        else
        {
            Debug.Log("Locked. Need " + requiredItemName);
        }
    }

    private void UnlockBorder()
    {
        unlocked = true;

        inventoryManager.RemoveItemByName(requiredItemName);

        if (blockingColliders != null)
        {
            foreach (Collider2D col in blockingColliders)
            {
                if (col != null)
                {
                    col.enabled = false;
                }
            }
        }

        if (linkedChest != null)
        {
            linkedChest.RefillChest();
        }

        Debug.Log("Unlocked with " + requiredItemName);
    }
}