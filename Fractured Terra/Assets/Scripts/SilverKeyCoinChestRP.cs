using UnityEngine;

public class SilverKeyCoinChestRP : MonoBehaviour, IInteractable
{
    public InventoryManager inventoryManager;
    public string requiredKeyName = "SilverKey";
    public int coinsGiven = 5;

    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool CanInteract()
    {
        if (isOpened) return false;
        if (inventoryManager == null) return false;

        InventoryItem keyItem = inventoryManager.FindItemByName(requiredKeyName);
        return keyItem != null;
    }

    public void Interact()
    {
        if (!CanInteract()) 
        {
            Debug.Log("Chest is locked. Need " + requiredKeyName);
            return;
        }

        OpenChest();
    }

    void OpenChest()
    {
        isOpened = true;

        if (animator != null)
        {
            animator.SetTrigger("OpenChest");
        }

        CoinManager.coinCount += coinsGiven;

        Debug.Log("Opened chest and got " + coinsGiven + " coins");
    }
}