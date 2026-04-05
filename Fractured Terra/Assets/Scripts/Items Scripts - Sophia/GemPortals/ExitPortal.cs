using UnityEngine;
using UnityEngine.SceneManagement;

// Attach to the exit portal. Starts locked until Unlock() is called (by GemPickup).
// When unlocked, player presses P nearby to travel to the hub world.
public class ExitPortal : MonoBehaviour
{
    public static ExitPortal Instance;

    [Tooltip("Name of the scene to load when the player enters the portal.")]
    public string sceneName = "Carylions Hubworld Backup";
    public float interactRadius = 1.5f;

    private bool isUnlocked = false;
    private Transform player;
    private SpriteRenderer sr;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
        if (ph != null) player = ph.transform;

        // Start visually locked (dimmed)
        SetLockedVisual();
    }

    void Update()
    {
        if (!isUnlocked || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= interactRadius && Input.GetKeyDown(KeyCode.P))
            Enter();
    }

    public void Unlock()
    {
        isUnlocked = true;
        if (sr != null)
        {
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }
        Debug.Log("[ExitPortal] Portal unlocked.");
    }

    void SetLockedVisual()
    {
        if (sr != null)
        {
            Color c = sr.color;
            c.a = 0.3f;
            sr.color = c;
        }
    }

    void Enter()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) Destroy(player);
        SceneManager.LoadScene(sceneName);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
