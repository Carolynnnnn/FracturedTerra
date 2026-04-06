using System.Collections;
using UnityEngine;

public class EnemyStatusRP : MonoBehaviour
{
    private MonoBehaviour movementScript; // not really used rn (leftover but harmless)
    private bool isFrozen = false; // tracks if enemy is currently frozen

    void Start()
    {
        movementScript = GetComponent<MonoBehaviour>();
    }

    public void Freeze(float duration)
    {
        if (!gameObject.activeInHierarchy) return; // safety check
        StartCoroutine(FreezeCoroutine(duration)); // starts freeze effect
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        if (isFrozen) yield break; // prevents stacking freeze
        isFrozen = true;

        // disables all scripts (movement, attack, etc) so enemy is fully frozen
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this && script != null && script.enabled)
            {
                script.enabled = false;
            }
        }

        yield return new WaitForSeconds(duration); // stays frozen for set time

        // re-enables everything after freeze ends
        MonoBehaviour[] scriptsAfter = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scriptsAfter)
        {
            if (script != this && script != null)
            {
                script.enabled = true;
            }
        }

        isFrozen = false;
    }

    public void CharmRemove()
    {
        Destroy(gameObject); // used for charm ability (basically removes enemy instead of killing normally)
    }
}