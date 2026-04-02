using System.Collections;
using UnityEngine;

using System.Collections;

using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackDuration = 0.2f;


    private InputAction attackAction;
    private InputAction useItemAction;
    private InputAction menuAction;

    private InputAction attackAction; // O key
    private InputAction useItemAction; // E key
    private InputAction menuAction; // M key
    private bool isAttacking = false;


    private bool isAttacking = false;

    private void Awake()
    {
        attackAction = new InputAction("Attack", binding: "<Keyboard>/o");
        useItemAction = new InputAction("UseItem", binding: "<Keyboard>/e");
        menuAction = new InputAction("Menu", binding: "<Keyboard>/m");

        attackAction.performed += ctx => Attack();
        useItemAction.performed += ctx => UseItem();
        menuAction.performed += ctx => OpenMenu();
    }

    private void OnEnable()
    {
        attackAction.Enable();
        useItemAction.Enable();
        menuAction.Enable();
    }

    private void OnDisable()
    {
        attackAction.Disable();
        useItemAction.Disable();
        menuAction.Disable();
    }

    private void Start()
    {
        if (attackHitbox != null)
        {
            attackHitbox.SetActive(false);
        }
    }

    private void Attack()
    {
        if (state != null && !state.canMove) return;
        if (isAttacking) return;

        Debug.Log("Attack pressed (O)");
        StartCoroutine(DoAttack());
    }

    private IEnumerator DoAttack()
    {
        isAttacking = true;

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        if (attackHitbox != null)
        {
            attackHitbox.SetActive(true);
        }

        yield return new WaitForSeconds(attackDuration);

        if (attackHitbox != null)
        {
            attackHitbox.SetActive(false);
        }

        isAttacking = false;
    }
    


    private void UseItem()
    {
        if (state != null && !state.canMove) return;
        Debug.Log("Use item pressed (E)");
    }

    private void OpenMenu()
    {
        Debug.Log("Menu/info pressed (M)");
    }
}