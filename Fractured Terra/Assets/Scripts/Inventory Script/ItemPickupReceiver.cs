using UnityEngine;

public class ItemPickupReceiver : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            WorldItem worldItem = other.GetComponent<WorldItem>();

            if (worldItem != null && inventoryManager != null)
            {
                inventoryManager.AddItem(worldItem.ToInventoryItem());
                Destroy(other.gameObject);
            }
        }
    }
}