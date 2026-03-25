using UnityEngine;

// NOTE: This class is no longer being used, as the inventory system was overhauled
// it remains here in fear that its deletion will cause an unforseen error!!
[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName; // Holds name of item
    public string itemDescription; // Holds item description
    public Sprite itemIcon; // Holds the sprite for the item
    // TODO: when implementing armor, consumables, and weapons make them subclasses of this one.
}
