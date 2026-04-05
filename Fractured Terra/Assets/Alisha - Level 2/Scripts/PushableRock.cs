using UnityEngine;

// Attach to Rock_0 (3). Requires a Rigidbody2D and a Collider2D.
// Rigidbody2D setup: Dynamic, Gravity Scale 0, Linear Damping 10,
//                   Constraints: Freeze Position Y + Freeze Rotation Z.
[RequireComponent(typeof(Rigidbody2D))]
public class PushableRock : MonoBehaviour
{
    [Tooltip("X position of the bridge gap where the rock should lock in place.")]
    [SerializeField] private float targetX;

    [Tooltip("Units per second the rock slides when pushed.")]
    [SerializeField] private float pushSpeed = 2f;

    [Tooltip("How close (in units) the rock needs to be to targetX before snapping.")]
    [SerializeField] private float snapThreshold = 0.2f;

    private Rigidbody2D _rb;
    private Collider2D _col;
    private bool _locked;
    private bool _beingPushed;
    private float _pushDirX;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_locked) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        // Read push direction from the player's current horizontal velocity
        Rigidbody2D playerRb = collision.rigidbody;
        if (playerRb == null) return;

        float vx = playerRb.linearVelocity.x;
        if (Mathf.Abs(vx) > 0.5f)
        {
            _beingPushed = true;
            _pushDirX = Mathf.Sign(vx);
        }
        else
        {
            _beingPushed = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _beingPushed = false;
    }

    private void FixedUpdate()
    {
        if (_locked) return;

        if (_beingPushed)
        {
            _rb.linearVelocity = new Vector2(_pushDirX * pushSpeed, 0f);

            // distToTarget is positive once the rock reaches or passes targetX in the push direction
            float distToTarget = (_rb.position.x - targetX) * _pushDirX;
            if (distToTarget >= -snapThreshold)
                Snap();
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }

    private void Snap()
    {
        // Freeze the rock permanently at the exact target position
        _rb.bodyType = RigidbodyType2D.Static;
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

        // Disable the collider so the player can walk over the rock to cross the bridge
        if (_col != null)
            _col.enabled = false;

        _locked = true;
        _beingPushed = false;
    }
}
