using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; // Closest interactable

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactableInRange?.Interact(); // Finds closest interactable in range and interacts with it (see below)
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) // When an interactable object is close
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // When player moves away from interactable object
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable ==  interactableInRange)
        {
            interactableInRange = null;
        }
    }
}
