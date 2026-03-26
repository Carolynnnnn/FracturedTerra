using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [Header("Item Data")]
    public string itemName = "New Item";
    public string description = "Item description";
    public Sprite itemIcon;
    public int maxLife = 3;
    public bool canUse = true;

    [Header("Drop / Pickup")]
    public GameObject worldPrefab;

    private void Reset()
    {
        worldPrefab = gameObject;
    }

    public InventoryItem ToInventoryItem()
    {
        return new InventoryItem(itemName, description, itemIcon, maxLife, canUse, worldPrefab);
    }
}