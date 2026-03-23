using UnityEngine;

[CreateAssetMenu(fileName = "NewShopStock", menuName = "Shop Stock")]
public class ShopStock : ScriptableObject // Set up for reusable single-item shop objs
{
    public int price; // Holds item price, in coins
    // TODO: IMPLEMENT ITEM OBJECT TYPE
    // notes: should include if consumable, effect, name/ID, sprite?
}
