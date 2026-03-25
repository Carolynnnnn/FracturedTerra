using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour // Handles attack, use item, and menu keys
{
    [SerializeField] private PlayerState state; // Reference to the PlayerState script

    private InputAction attackAction; // O key
    private InputAction useItemAction; // E key
    private InputAction menuAction; // M key

    private void Awake() // Runs when the script first loads
    {
        attackAction = new InputAction("Attack", binding: "<Keyboard>/o"); // O for attack
        useItemAction = new InputAction("UseItem", binding: "<Keyboard>/e"); // E for use item
        menuAction = new InputAction("Menu", binding: "<Keyboard>/m"); // M for menu/info

        attackAction.performed += ctx => Attack(); // Calls Attack() when O is pressed
        useItemAction.performed += ctx => UseItem(); // Calls UseItem() when E is pressed
        menuAction.performed += ctx => OpenMenu(); // Calls OpenMenu() when M is pressed
    }
    
    private void OnEnable() // Enables actions when object becomes active
    {
        attackAction.Enable(); // Turn on attack input
        useItemAction.Enable(); // Turn on use-item input
        menuAction.Enable(); // Turn on menu input
    }


    private void OnDisable() // Disables actions when object becomes inactive
    {
        attackAction.Disable(); // Turn off attack input
        useItemAction.Disable(); // Turn off use-item input
        menuAction.Disable(); // Turn off menu input
    }

    

    private void Attack() // Handles attack key
    {
        if (state != null && !state.canMove) return; // Optional safety check
        Debug.Log("Attack pressed (O)"); // Placeholder for attack logic
    }


    private void UseItem() // Handles use item key
    {
        if (state != null && !state.canMove) return; // Optional safety check
        Debug.Log("Use item pressed (E)"); // Placeholder for use-item logic
    }


    private void OpenMenu() // Handles menu/info key
    {
        Debug.Log("Menu/info pressed (M)"); // Placeholder for menu logic
    }
}
    
