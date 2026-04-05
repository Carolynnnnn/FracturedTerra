using UnityEngine;

public class GemPickup : MonoBehaviour
{
    public float pickupRadius = 1.5f;

    private Transform player;

    void Start()
    {
        PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
        if (ph != null) player = ph.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= pickupRadius && Input.GetKeyDown(KeyCode.P))
        {
            GemManager.gemCount++;

            if (ExitPortal.Instance != null)
                ExitPortal.Instance.Unlock();

            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
