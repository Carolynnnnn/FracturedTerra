using UnityEngine;

public class LuxLightFragment : MonoBehaviour
{
    [SerializeField] private GameObject sanctumDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // If this is the last remaining fragment, unlock the sanctum door
        LuxLightFragment[] remaining = FindObjectsOfType<LuxLightFragment>();
        if (remaining.Length <= 1 && sanctumDoor != null)
            sanctumDoor.SetActive(false);

        Destroy(gameObject);
    }
}
