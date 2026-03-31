using UnityEngine;
public class OrderInLayer : MonoBehaviour
{
    public SpriteRenderer[] objectRenderers;
    private SpriteRenderer[] playerRenderers;
    private int[] initialOrders;
    public float feetOffset = -0.5f;
    public float triggerDistance = 5f;
    void Start()
    {
        playerRenderers = GetComponentsInChildren<SpriteRenderer>();
        initialOrders = new int[playerRenderers.Length];
        for (int i = 0; i < playerRenderers.Length; i++)
        {
            initialOrders[i] = playerRenderers[i].sortingOrder;
        }
    }
    void Update()
    {
        float playerFeetY = transform.position.y + feetOffset;
        bool nearHouse = false;

        foreach (SpriteRenderer obj in objectRenderers)
        {
            if (obj == null) continue;

            float distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance > triggerDistance) continue;

            nearHouse = true;
            float houseBottomY = obj.transform.position.y - obj.bounds.size.y / 2f;

            if (playerFeetY < houseBottomY)
                SetPlayerOrder(obj.sortingOrder + 1);
            else
                SetPlayerOrder(obj.sortingOrder - 1);
        }

        if (!nearHouse)
            RestoreInitialOrders();
    }
    void SetPlayerOrder(int order)
    {
        for (int i = 0; i < playerRenderers.Length; i++)
        {
            playerRenderers[i].sortingOrder = order + (initialOrders[i] - initialOrders[0]);
        }
    }
    void RestoreInitialOrders()
    {
        for (int i = 0; i < playerRenderers.Length; i++)
        {
            playerRenderers[i].sortingOrder = initialOrders[i];
        }
    }
}