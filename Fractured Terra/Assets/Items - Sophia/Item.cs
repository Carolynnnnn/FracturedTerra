using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName; // Holds name of item
    public string itemDescription; // Holds item description
    public Sprite itemIcon; // Holds the sprite for the item
    // TODO: when implementing armor, consumables, and weapons make them subclasses of this one.
}
