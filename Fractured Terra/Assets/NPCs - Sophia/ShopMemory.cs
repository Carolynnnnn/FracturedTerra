using System.Collections.Generic;
using UnityEngine;

public static class ShopMemory
{
    private static Dictionary<ShopStock, int> stockPurchased = new Dictionary<ShopStock, int>(); // Tracks number of item bought from shop
    
    public static void Buy(ShopStock stock) // Records a purchase
    {
        if (!stockPurchased.ContainsKey(stock)) stockPurchased[stock] = 0; // Initialize field when it doesn't exist yet
        stockPurchased[stock]++; // Adds one to the amount purchased
    }
    
    public static int Bought(ShopStock stock) // Check how many purchases have been made at a shop
    {
        if (!stockPurchased.ContainsKey(stock)) return 0;
        return stockPurchased[stock];
    }
}
