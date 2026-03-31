using System;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour // Coins exchanged at shops for goods
{
    public static int coinCount = 0; // Keeps track of amount of coins the player has
    public TMP_Text coinUI; // Shows player how many coins they have
        
    public void Update()
    {
        if (coinUI != null) coinUI.SetText(coinCount.ToString()); // Keep UI updated
    }
}
