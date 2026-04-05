using UnityEngine;

// Drop-in replacement for EnemyMovement on the Wolf Enemy.
// Uses Rigidbody2D.MovePosition() so the physics engine stays in sync
// and OnTriggerStay2D on the AttackRange child fires correctly.
[RequireComponent(typeof(Rigidbody2D))]
public class WolfMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float moveDistance = 2f;

    private Rigidbody2D _rb;
    private Vector2 _startPos;
    private bool _movingRight = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = _rb.position;
    }

    private void FixedUpdate()
    {
        Vector2 current = _rb.position;

        if (_movingRight)
        {
            _rb.MovePosition(current + Vector2.right * speed * Time.fixedDeltaTime);

            if (current.x >= _startPos.x + moveDistance)
                _movingRight = false;
        }
        else
        {
            _rb.MovePosition(current + Vector2.left * speed * Time.fixedDeltaTime);

            if (current.x <= _startPos.x - moveDistance)
                _movingRight = true;
        }
    }
}
