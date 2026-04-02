using UnityEngine;
using System.Collections;

public class BreakableVase : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D col;
    private bool isBroken = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Break()
    {
        if (isBroken) return;
        isBroken = true;
        StartCoroutine(BreakRoutine());
    }

    IEnumerator BreakRoutine()
    {
        if (sr != null)
        {
            sr.color = new Color(1f, 1f, 1f, 0.4f);
        }

        if (col != null)
        {
            col.enabled = false;
        }

        yield return new WaitForSeconds(0.08f);

        Destroy(gameObject);
    }
}
