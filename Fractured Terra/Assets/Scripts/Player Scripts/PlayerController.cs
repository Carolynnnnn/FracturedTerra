using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    // Editor data
    // Movement Attributes 
    [SerializeField] float _moveSpeed = 6;
    // Dependencies
    [SerializeField] Rigidbody2D _rb;
    // [SerializeField] private Animator _animator; // Animate movement! (not used yet)
    
    [SerializeField] PlayerState _state; // RP add
    
    // Internal data
    private Vector2 _moveDir = Vector2.zero;
    private InputAction _moveAction;
    public bool CanMove = true; // Helps prevent movement when dialogue is open
    
    // Keybinds
    private void Awake()
    {
        // Create a 2DVector composite binding for WASD
        _moveAction = new InputAction("Move", InputActionType.Value);
        _moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        
        // Update move direction
       // Old Line:" _moveAction.performed += ctx => _moveDir = ctx.ReadValue<Vector2>(); "
       _moveAction.performed += ctx =>
       {
           _moveDir = ctx.ReadValue<Vector2>();

           if (_state != null && _moveDir != Vector2.zero)
           {
               _state.lastMoveDir = _moveDir;
           }
       }; // RP update
        
       //Old Line:" _moveAction.canceled += ctx => _moveDir = Vector2.zero; "
       _moveAction.canceled += ctx =>
       {
           _moveDir = Vector2.zero;
       }; // RP update
    }
    
    // Input actions
    private void OnEnable()
    {
        _moveAction.Enable();
    }
    private void OnDisable()
    {
        _moveAction.Disable();
    }
    
    // Tick
    private void FixedUpdate() // Used for physics system
    {
        if (_state != null)
        {
            _state.canMove = CanMove;
        } // RP add
        
        if (!CanMove) // When a dialogue is open, don't change position
        {
            _rb.linearVelocity = Vector2.zero; // stop instantly
            // _animator.SetFloat("Speed", 0);
            
            if (_state != null)
            {
                _state.isMoving = false;
                _state.isRunning = false;
            } // RP add
            
            return;
        }
        
        _rb.linearVelocity = _moveDir.normalized * _moveSpeed;
        
        // Update animation (not used yet)
        /*
        _animator.SetFloat("MoveX", _moveDir.x);
        _animator.SetFloat("MoveY", _moveDir.y);
        _animator.SetFloat("Speed", _moveDir.sqrMagnitude);
        */
        
        if (_state != null)
        {
            _state.isMoving = _moveDir != Vector2.zero;
            _state.isRunning = false;
        } // RP add
    }
}
