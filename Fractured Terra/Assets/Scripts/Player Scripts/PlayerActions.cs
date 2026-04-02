using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour // Handles attack, use item, and menu keys
{
    [SerializeField] private PlayerState state; // Reference to the PlayerState script
    [SerializeField] private GameObject attackHitbox; // Hitbox object that turns on during attack
    [SerializeField] private Animator animator; // Animator that plays the attack animation
    [SerializeField] private float attackDuration = 0.2f; // How long the attack hitbox stays active

    private InputAction attackAction; // O key
    private InputAction useItemAction; // E key
    private InputAction menuAction; // M key
    private bool isAttacking = false;

    private void Awake() // Runs when the script first loads
    {
        attackAction = new InputAction("Attack", binding: "<Keyboard>/o"); // O for attack
        useItemAction = new InputAction("UseItem", binding: "<Keyboard>/e"); // E for use item
        menuAction = new InputAction("Menu", binding: "<Keyboard>/m"); // M for menu/info

        attackAction.performed += ctx => Attack(); // Calls Attack() when O key is pressed
        useItemAction.performed += ctx => UseItem(); // Calls UseItem() when E key is pressed
        menuAction.performed += ctx => OpenMenu(); // Calls OpenMenu() when M key is pressed
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
        if (attackHitbox != null) // Make sure attack hitbox exists
        {
            attackHitbox.SetActive(false); // Keep it off until attacking
        }
    }

    private void Attack() // Handles attack input
    {
        if (state != null && !state.canMove) return; // Optional safety check
        if (isAttacking) return; // Prevent starting another attack while already attacking

        Debug.Log("Attack pressed (O)"); // Debug message
        StartCoroutine(DoAttack()); // Start timed attack sequence
    }

    private IEnumerator DoAttack() // Plays animation and turns hitbox on briefly
    {
        isAttacking = true; // Mark attack active

        if (animator != null) // Make sure animator exists
        {
            animator.SetTrigger("Attack"); // Play attack animation
        }

        if (attackHitbox != null) // Make sure hitbox exists
        {
            attackHitbox.SetActive(true); // Turn on hitbox
        }

        yield return new WaitForSeconds(attackDuration); // Wait during attack window

        if (attackHitbox != null) // Make sure hitbox still exists
        {
            attackHitbox.SetActive(false); // Turn off hitbox
        }

        isAttacking = false; // Attack finished
    }
    


    private void UseItem() // Handles use item input
    {
        if (state != null && !state.canMove) return; // Optional safety check
        Debug.Log("Use item pressed (E)"); // Placeholder for item logic
    }


    private void OpenMenu() // Handles menu/info key
    {
        Debug.Log("Menu/info pressed (M)"); // Placeholder for menu logic
    }
    
}
    
