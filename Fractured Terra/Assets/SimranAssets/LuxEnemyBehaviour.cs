using UnityEngine;

// Combines Alisha's WolfMovement and WolfAttack patterns into one script
// for Simran's wolf enemies in Simran_Level5.
//
// Movement (WolfMovement pattern): uses Rigidbody2D.MovePosition() so the
// physics engine stays in sync and OnTriggerStay2D fires correctly.
// Patrols left-right when the player is out of detection range; chases
// the player with the same MovePosition approach when they are close.
//
// Attack (WolfAttack pattern): OnTriggerStay2D on the wolf's AttackRange
// trigger child, fired via the root Rigidbody2D. Uses GetComponentInParent
// so PlayerHealth is found even when the colliding object is a child of
// the Player.
[RequireComponent(typeof(Rigidbody2D))]
public class LuxEnemyBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float patrolDistance = 2f;
    [SerializeField] private float detectionRange = 5f;

    [Header("Attack")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private Rigidbody2D _rb;
    private Vector2 _startPos;
    private bool _movingRight = true;
    private Transform _player;
    private float _lastAttackTime;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = _rb.position;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;
    }

    private void FixedUpdate()
    {
        if (_player == null) return;

        float dist = Vector2.Distance(_rb.position, (Vector2)_player.position);

        if (dist <= detectionRange)
        {
            // Chase: move toward player using MovePosition (Alisha's WolfMovement approach)
            Vector2 dir = ((Vector2)_player.position - _rb.position).normalized;
            _rb.MovePosition(_rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Patrol: left-right within patrolDistance (Alisha's WolfMovement patrol logic)
            Vector2 current = _rb.position;

            if (_movingRight)
            {
                _rb.MovePosition(current + Vector2.right * moveSpeed * Time.fixedDeltaTime);
                if (current.x >= _startPos.x + patrolDistance)
                    _movingRight = false;
            }
            else
            {
                _rb.MovePosition(current + Vector2.left * moveSpeed * Time.fixedDeltaTime);
                if (current.x <= _startPos.x - patrolDistance)
                    _movingRight = true;
            }
        }
    }

    // Fires from the AttackRange child's trigger via the root Rigidbody2D.
    // Mirrors Alisha's WolfAttack.cs exactly.
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponentInParent<PlayerHealth>();

        if (player != null && Time.time >= _lastAttackTime + attackCooldown)
        {
            player.TakeDamage(damage);
            _lastAttackTime = Time.time;
        }
    }
}
