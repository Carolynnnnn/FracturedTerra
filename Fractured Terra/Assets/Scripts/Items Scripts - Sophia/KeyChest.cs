using UnityEngine;

// A chest that spawns a key when opened.
//
// Setup:
//   1. Create Empty → name it "KeyChest"
//   2. Add SpriteRenderer → assign the closed chest sprite (chest_07_0)
//   3. Attach this script
//   4. In the Inspector assign:
//        Closed Sprite  → chest_07_0
//        Open Sprite    → any open-chest sprite you have (or leave blank to just hide chest)
//        Key Sprite     → your key sprite (e.g. Key1-GOLD frame)
//        Pickup Radius  → how close the player needs to be
public class KeyChest : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite closedSprite;
    public Sprite openSprite;

    [Header("Key")]
    [Tooltip("Sprite to use for the key that appears when the chest opens.")]
    public Sprite keySprite;
    [Tooltip("Offset from the chest where the key appears.")]
    public Vector2 keyOffset = new Vector2(0.5f, 0f);
    [Tooltip("Scale of the spawned key. Increase if the key appears too small.")]
    public float keyScale = 3f;
    [Tooltip("How close the player needs to be to interact.")]
    public float interactRadius = 1.5f;

    private SpriteRenderer sr;
    private Transform player;
    private bool isOpen = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (closedSprite != null) sr.sprite = closedSprite;

        PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
        if (ph != null) player = ph.transform;
    }

    void Update()
    {
        if (isOpen || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= interactRadius && Input.GetKeyDown(KeyCode.P))
            OpenChest();
    }

    void OpenChest()
    {
        if (CoinManager.coinCount < 5)
        {
            Debug.Log("[KeyChest] Not enough coins to open chest.");
            return;
        }

        CoinManager.coinCount -= 5;
        isOpen = true;

        if (openSprite != null)
            sr.sprite = openSprite;

        GameObject key = new GameObject("Key");
        key.transform.position = (Vector2)transform.position + keyOffset;
        key.transform.localScale = Vector3.one * keyScale;

        SpriteRenderer keySr = key.AddComponent<SpriteRenderer>();
        keySr.sprite = keySprite;
        keySr.sortingLayerName = sr.sortingLayerName;
        keySr.sortingOrder = sr.sortingOrder;

        key.AddComponent<KeyPickup>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
