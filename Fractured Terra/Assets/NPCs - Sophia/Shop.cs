using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    public ShopStock stock; // What the shop sells
    public InventoryManager inventoryManager; // Keeps track of player's inventory
    
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (ShopMemory.Bought(stock) == stock.maxStock) Debug.Log("There are no " + stock.itemName + "s left in stock.");
        else TryBuy(); // Buys an item, if player has enough coins
    }
    
    void TryBuy()
    {
        if (CoinManager.coinCount >= stock.price)
        {
            CoinManager.coinCount -= stock.price; // Takes away player's coins
            GiveItem(); // Give player the item
            ShopMemory.Buy(stock); // Save in shop memory
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
