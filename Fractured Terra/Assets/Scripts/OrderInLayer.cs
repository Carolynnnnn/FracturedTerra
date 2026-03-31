using UnityEngine;
public class OrderInLayer : MonoBehaviour
{
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        if (sr == null) return;
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}