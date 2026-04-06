using UnityEngine;

public class GemChest : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite closedSprite;
    public Sprite openSprite;

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
        if (isOpen) return;

        if (TimeLimitManager.Instance == null || TimeLimitManager.Instance.KeysCollected < 3)
        {
            Debug.Log("[GemChest] Need all 3 keys to open this chest.");
            return;
        }

        isOpen = true;

        if (openSprite != null)
            sr.sprite = openSprite;

        // Award gem directly
        GemManager.gemCount++;

        // Unlock exit portal
        if (ExitPortal.Instance != null)
            ExitPortal.Instance.Unlock();

        // Unlock ability
        if (AbilityUnlockManagerRP.Instance != null)
            AbilityUnlockManagerRP.Instance.UnlockAbility(2);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
