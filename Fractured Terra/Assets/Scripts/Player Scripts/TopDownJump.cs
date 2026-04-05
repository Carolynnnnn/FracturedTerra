using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

// When the player presses Space, they can physically move through hazard objects
// for jumpDuration seconds. Uses Physics2D.IgnoreLayerCollision to toggle whether
// the Player layer and Hazard layer collide.
//
// Setup:
//   1. In Edit > Project Settings > Tags and Layers, create a layer called "Hazard".
//   2. Assign all hazard objects (pits, spikes, etc.) to the Hazard layer.
//   3. On this component, set Hazard Layer Name to "Hazard" (or whatever you named it).
public class TopDownJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [Tooltip("How long the player passes through hazards after pressing Space.")]
    [SerializeField] private float jumpDuration = 2f;
    [Tooltip("Seconds before the player can jump again.")]
    [SerializeField] private float jumpCooldown = 0.5f;

    [Header("Layers")]
    [Tooltip("Name of the layer assigned to hazard objects the player should jump over.")]
    [SerializeField] private string hazardLayerName = "Hazard";

    private PlayerState state;
    private InputAction jumpAction;
    private float lastJumpTime = -999f;
    private int playerLayer;
    private int hazardLayer;

    private void Awake()
    {
        state = GetComponent<PlayerState>();
        playerLayer = gameObject.layer;
        hazardLayer = LayerMask.NameToLayer(hazardLayerName);

        if (hazardLayer == -1)
            Debug.LogWarning($"[TopDownJump] Layer \"{hazardLayerName}\" not found. " +
                "Create it in Edit > Project Settings > Tags and Layers.");

        jumpAction = new InputAction("Jump", binding: "<Keyboard>/space");
        jumpAction.performed += _ => TryJump();
    }

    private void OnEnable()  => jumpAction.Enable();
    private void OnDisable() => jumpAction.Disable();

    private void TryJump()
    {
        if (state != null && state.isJumping) return;
        if (state != null && !state.canMove) return;
        if (Time.time - lastJumpTime < jumpCooldown) return;
        if (hazardLayer == -1) return;

        StartCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRoutine()
    {
        lastJumpTime = Time.time;
        if (state != null) state.isJumping = true;

        // Allow player to physically pass through hazard objects
        Physics2D.IgnoreLayerCollision(playerLayer, hazardLayer, true);

        yield return new WaitForSeconds(jumpDuration);

        // Restore hazard collisions
        Physics2D.IgnoreLayerCollision(playerLayer, hazardLayer, false);

        if (state != null) state.isJumping = false;
    }
}
