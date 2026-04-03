using UnityEngine;

public class Buyer : MonoBehaviour, IInteractable
{
    public ShopStock stock; // What the buyer buys, only needs a name and a price
    public InventoryManager inventoryManager; // Keeps track of player's inventory
        
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        TrySell();
    }

    public void TrySell()
    {
        bool hadItem = inventoryManager.RemoveItemByName(stock.itemName); // Removes the item being sold from the player's inventory
        if (hadItem)
        {
            CoinManager.coinCount += stock.price; // Gives player money in exchange for item
            Debug.Log("Sold " + stock.itemName);
        }
        else Debug.Log(stock.itemName + " not in inventory.");
    }
}
