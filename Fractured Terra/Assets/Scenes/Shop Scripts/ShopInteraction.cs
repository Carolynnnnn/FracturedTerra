using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private float interactionDistance = 2.5f;

    private bool shopOpen = false;

    private void Start()
    {
        if (shopUI != null)
            shopUI.SetActive(false);

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null || shopUI == null)
            return;

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.P))
        {
            shopOpen = !shopOpen;
            shopUI.SetActive(shopOpen);
        }
    }
}