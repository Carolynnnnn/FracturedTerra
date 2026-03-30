using TMPro;
using UnityEngine;

public class GemManager : MonoBehaviour // Gems are items related to level progression
{
    public static int gemCount = 0; // Keeps track of amount of gems the player has
    public TMP_Text gemUI; // Shows player how many gems they have

    public void Update()
    {
        if (gemUI != null) gemUI.SetText(gemCount.ToString()); // Keep UI updated
    }
}
