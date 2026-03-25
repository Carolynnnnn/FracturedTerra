using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    public ShopStock stock; // What the shop sells
    public CoinManager cm; // Keeps track of how many coins the player has
    public InventoryManager inventoryManager; // Keeps track of player's inventory
    
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        TryBuy(); // Buys an item, if player has enough coins
    }
    
    void TryBuy()
    {
        if (cm.coinCount >= stock.price)
        {
            cm.coinCount -= stock.price; // Takes away player's coins
            GiveItem(); // Give player the item
        }
    }
    
    void GiveItem()
    {
        if (inventoryManager == null) // Catch potential errors
        {
            Debug.LogWarning("No InventoryManager assigned!");
            return;
        }
        
        InventoryItem newItem = new InventoryItem( // Create new inventory item based on stock
            stock.itemName,
            stock.description,
            stock.icon,
            stock.maxLife,
            stock.canUse,
            stock.worldPrefab
        );

        inventoryManager.AddItem(newItem); // Adds item to inventory
        Debug.Log("Bought " + stock.itemName); // Debugging
    }
    
}
