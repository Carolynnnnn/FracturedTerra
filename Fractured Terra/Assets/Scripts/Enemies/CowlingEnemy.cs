using System.Collections;
using UnityEngine;

// Cowling_1 enemy. Idles until the player enters detection range,
// then chases and deals melee damage on contact.
//
// Required components on this GameObject:
//   - Rigidbody2D  (Body Type: Dynamic, Gravity Scale: 0, Freeze Rotation Z)
//   - CircleCollider2D
//   - SpriteRenderer
//   - EnemyHealthRP
//   - EnemyStatusRP
public class CowlingEnemy : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 20f;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Attack")]
    public float attackRange = 0.8f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    [Header("Animation")]
    [Tooltip("Frames to cycle while chasing the player.")]
    public Sprite[] walkFrames;
    [Tooltip("Frames to play when attacking.")]
    public Sprite[] attackFrames;
    [Tooltip("Seconds per frame.")]
    public float frameRate = 0.1f;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isChasing = false;
    private float lastAttackTime = -999f;

    // Animation state
    private Sprite[] currentAnim;
    private int frameIndex = 0;
    private float frameTimer = 0f;
    private bool isPlayingAttack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
        if (ph != null)
            player = ph.transform;
        else
            Debug.LogWarning("[CowlingEnemy] No PlayerHealth found in scene.");

        SetAnim(walkFrames);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (!isChasing && dist <= detectionRange)
            isChasing = true;

        if (!isChasing) return;

        if (sr != null)
            sr.flipX = player.position.x < transform.position.x;

        if (dist <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            Attack();

        TickAnimation();
    }

    void FixedUpdate()
    {
        if (player == null || !isChasing) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Switch back to walk anim while moving
        if (!isPlayingAttack)
            SetAnim(walkFrames);

        Vector2 dir = ((Vector2)player.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDamage(attackDamage);

        // Play attack animation then return to walk
        if (attackFrames != null && attackFrames.Length > 0)
            StartCoroutine(PlayAttackAnim());
    }

    IEnumerator PlayAttackAnim()
    {
        isPlayingAttack = true;
        SetAnim(attackFrames);

        // Wait for the full attack animation to finish once
        yield return new WaitForSeconds(frameRate * attackFrames.Length);

        isPlayingAttack = false;
        SetAnim(walkFrames);
    }

    void SetAnim(Sprite[] frames)
    {
        if (frames == currentAnim) return;
        currentAnim = frames;
        frameIndex = 0;
        frameTimer = 0f;
    }

    void TickAnimation()
    {
        if (currentAnim == null || currentAnim.Length == 0) return;

        frameTimer += Time.deltaTime;
        if (frameTimer >= frameRate)
        {
            frameTimer = 0f;
            frameIndex = (frameIndex + 1) % currentAnim.Length;
            if (sr != null)
                sr.sprite = currentAnim[frameIndex];
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
