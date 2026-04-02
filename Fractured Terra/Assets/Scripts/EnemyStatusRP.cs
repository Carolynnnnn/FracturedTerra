using System.Collections;
using UnityEngine;

public class EnemyStatusRP : MonoBehaviour
{
    private MonoBehaviour movementScript;
    private bool isFrozen = false;

    void Start()
    {
        movementScript = GetComponent<MonoBehaviour>();
    }

    public void Freeze(float duration)
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        if (isFrozen) yield break;
        isFrozen = true;

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this && script != null && script.enabled)
            {
                script.enabled = false;
            }
        }

        yield return new WaitForSeconds(duration);

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
        Destroy(gameObject);
    }
}