using UnityEngine;

public class TrapChestRP : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool CanInteract()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        OpenTrapChest();
    }

    void OpenTrapChest()
    {
        isOpened = true;

        if (animator != null)
        {
            animator.SetTrigger("OpenChest");
        }

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerHealth.currentHealth);
            }
        }

        Debug.Log("Trap chest opened - player died");
    }
}