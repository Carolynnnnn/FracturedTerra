using UnityEngine;

using UnityEngine.InputSystem;

using System.Collections; 

public class PlayerActions : MonoBehaviour // Handles attack, use item, and menu keys
{
    [SerializeField] private PlayerState state; // Reference to the PlayerState script
    [SerializeField] private GameObject attackHitbox; // The hitbox object that turns on during attack
    [SerializeField] private float attackDuration = 0.2f; // How long the attack hitbox stays active

    private InputAction attackAction; // O key
    private InputAction useItemAction; // E key
    private InputAction menuAction; // M key

    private bool isAttacking = false; // Prevents attack spam overlap

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

    private void Start() // Runs before the first frame update
    {
        if (attackHitbox != null) // Make sure the hitbox exists
        {
            attackHitbox.SetActive(false); // Keep hitbox off until attacking
        }
    }

    private void Attack() // Handles attack key
    {
        if (state != null && !state.canMove) return; // Optional safety check
        if (isAttacking) return; // Prevent starting another attack while already attacking

        Debug.Log("Attack pressed (O)"); // Debug message
        StartCoroutine(DoAttack()); // Start timed attack
    }

    private IEnumerator DoAttack() // Turns hitbox on briefly, then off
    {
        isAttacking = true; // Mark attack active

        if (attackHitbox != null) // Make sure hitbox exists
        {
            attackHitbox.SetActive(true); // Turn on hitbox
        }

        yield return new WaitForSeconds(attackDuration); // Wait for attack duration

        if (attackHitbox != null) // Make sure hitbox still exists
        {
            attackHitbox.SetActive(false); // Turn hitbox off
        }

        isAttacking = false; // Attack finished
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
