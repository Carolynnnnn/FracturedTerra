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
}
    
