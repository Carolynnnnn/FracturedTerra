using UnityEngine;

// Attach to a key GameObject. When the player stands near it and presses P,
// the key disappears (is collected).
public class KeyPickup : MonoBehaviour
{
    [Tooltip("How close the player needs to be to collect the key.")]
    public float pickupRadius = 1.5f;

    private Transform player;

    void Start()
    {
        PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
        if (ph != null)
            player = ph.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= pickupRadius && Input.GetKeyDown(KeyCode.P))
            Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
