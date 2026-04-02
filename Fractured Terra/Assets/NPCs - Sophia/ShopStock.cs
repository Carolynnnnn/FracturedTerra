using UnityEngine;

[CreateAssetMenu(fileName = "NewShopStock", menuName = "Shop Stock")]
public class ShopStock : ScriptableObject // Set up for reusable single-item shop objs
{
    public int price; // Holds item price, in coins
    public int maxStock = -1; // Determines how many items are stocked and available for purchase
                              // negative value means stock is infinite
    
    // Build item being sold
    public string itemName; // Item's name
    public string description; // Item's description
    public Sprite icon; // Item's sprite
    public int maxLife = 1; // Max amount of uses an item has, 1 use by default
    public bool canUse; // Determines if the item can be used
    public GameObject worldPrefab; // Helps drop item if necessary
                                   // note: event items SHOULD NOT HAVE THIS, as they should never be dropped
}
