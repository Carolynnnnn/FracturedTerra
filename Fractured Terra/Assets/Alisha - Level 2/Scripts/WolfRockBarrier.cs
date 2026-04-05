using UnityEngine;

// Attach to the Wolf Enemy GameObject.
// Assign the three Rock GameObjects in the Inspector.
// When the wolf is destroyed (dies), all assigned rocks are disabled.
public class WolfRockBarrier : MonoBehaviour
{
    [SerializeField] private GameObject[] rocks;

    private void OnDestroy()
    {
        // Guard against this firing during scene unload
        // (when rock GameObjects may already be destroyed)
        if (!gameObject.scene.isLoaded) return;

        foreach (GameObject rock in rocks)
        {
            if (rock != null)
                rock.SetActive(false);
        }
    }
}
