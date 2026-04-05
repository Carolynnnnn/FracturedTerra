using UnityEngine;

// A chest that only opens once all 3 keys have been collected.
// Opens and spawns a gem beside it when the player presses P.
//
// Setup:
//   1. Create Empty → name it "GemChest"
//   2. Add SpriteRenderer → assign your closed chest sprite
//   3. Attach this script
//   4. Fill in the Inspector fields
public class GemChest : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite closedSprite;
    public Sprite openSprite;

    [Header("Gem")]
    [Tooltip("Sprite to use for the gem that appears when the chest opens.")]
    public Sprite gemSprite;
    [Tooltip("Offset from the chest where the gem appears.")]
    public Vector2 gemOffset = new Vector2(0.5f, 0f);
    [Tooltip("Scale of the spawned gem.")]
    public float gemScale = 3f;

    [Header("Interaction")]
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
            TryOpen();
    }

    void TryOpen()
    {
        // Check all 3 keys have been collected
        int keys = TimeLimitManager.Instance != null ? TimeLimitManager.Instance.KeysCollected : -1;
        Debug.Log($"[GemChest] TryOpen called. TimeLimitManager found: {TimeLimitManager.Instance != null}, Keys: {keys}, Gem sprite assigned: {gemSprite != null}, AbilityUnlockManager found: {AbilityUnlockManagerRP.Instance != null}");

        if (TimeLimitManager.Instance == null || keys < 3)
        {
            Debug.Log("[GemChest] Need all 3 keys to open this chest.");
            return;
        }

        isOpen = true;

        if (openSprite != null)
            sr.sprite = openSprite;

        // Unlock ability
        if (AbilityUnlockManagerRP.Instance != null)
            AbilityUnlockManagerRP.Instance.UnlockAbility(2);
        else
            Debug.LogWarning("[GemChest] AbilityUnlockManagerRP.Instance is null — ability not unlocked.");

        // Spawn gem beside the chest
        GameObject gem = new GameObject("Gem");
        gem.transform.position = (Vector2)transform.position + gemOffset;
        gem.transform.localScale = Vector3.one * gemScale;

        SpriteRenderer gemSr = gem.AddComponent<SpriteRenderer>();
        gemSr.sprite = gemSprite;
        gemSr.sortingLayerName = sr.sortingLayerName;
        gemSr.sortingOrder = sr.sortingOrder;

        gem.AddComponent<GemPickup>();
        Debug.Log($"[GemChest] Gem spawned at {gem.transform.position}, sprite: {gemSprite}");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
