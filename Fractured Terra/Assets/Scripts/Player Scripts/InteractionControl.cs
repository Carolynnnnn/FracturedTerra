using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionControl : MonoBehaviour
{
    [SerializeField] private InteractionDetector _interactionDetector;

    private InputAction _interactAction;

    private void Awake()
    {
        // Create interact action bound to P key
        _interactAction = new InputAction("Interact", InputActionType.Button);
        _interactAction.AddBinding("<Keyboard>/p");

        // Call interact method
        _interactAction.performed += ctx =>
        {
            _interactionDetector.OnInteract(ctx);
        };
    }

    private void OnEnable()
    {
        _interactAction.Enable();
    }

    private void OnDisable()
    {
        _interactAction.Disable();
    }
}