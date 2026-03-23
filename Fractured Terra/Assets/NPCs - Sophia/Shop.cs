using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    public ShopStock stock; // What the shop sells
    public CoinManager cm; // Keeps track of how many coins the player has

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
        if (cm.cointCount >= stock.price)
        {
            cm.cointCount -= stock.price; // Takes away player's coins
            GiveItem(); // Give player the item
        }
    }
    
    void GiveItem()
    {
        Debug.Log("item bought"); // TODO: Implement inventory and item class, give player the item, remove this debug line
    }
    
}
