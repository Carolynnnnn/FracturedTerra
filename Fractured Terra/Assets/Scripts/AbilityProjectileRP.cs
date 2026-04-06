using UnityEngine;

public class AbilityProjectileRP : MonoBehaviour
{
    public float speed = 8f; // how fast the projectile moves
    public float lifetime = 0.6f; // destroys itself after a short time so it doesn’t fly forever
    public int damage = 1; // damage dealt when it hits an enemy
    public LayerMask enemyLayer; // what counts as an enemy
    public LayerMask breakableLayer; // what counts as a breakable object like vases

    private Vector2 direction = Vector2.right; // default direction in case nothing else is set
    private bool hasHitSomething = false; // stops it from hitting multiple things at once

    private Collider2D projectileCollider;
    private Rigidbody2D projectileRb;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // makes sure the projectile always moves in a clean direction
    }

    void Start()
    {
        projectileCollider = GetComponent<Collider2D>();
        projectileRb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifetime); // auto delete after a bit
    }

    void Update()
    {
        if (hasHitSomething) return; // once it hits, stop moving it

        transform.position += (Vector3)(direction * speed * Time.deltaTime); // moves the projectile forward
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHitSomething) return; // prevents double hits / glitchy collisions

        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            FinalBossHealthRP boss = other.GetComponent<FinalBossHealthRP>();
            if (boss != null)
            {
                boss.TakeDamage(damage); // lets projectiles damage the final boss
            }
            else
            {
                EnemyHealthRP enemy = other.GetComponent<EnemyHealthRP>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage); // regular enemy damage
                }
            }

            StopProjectileAndFinishAnimation(); // stop movement but let the hit effect finish
            return;
        }

        if (((1 << other.gameObject.layer) & breakableLayer) != 0)
        {
            if (other.CompareTag("breakitem"))
            {
                Destroy(other.gameObject); // breaks things like vases
            }

            StopProjectileAndFinishAnimation();
        }
    }

    void StopProjectileAndFinishAnimation()
    {
        hasHitSomething = true; // marks projectile as done so it can’t hit again

        if (projectileCollider != null)
        {
            projectileCollider.enabled = false; // turn off collision after impact
        }

        if (projectileRb != null)
        {
            projectileRb.linearVelocity = Vector2.zero;
            projectileRb.angularVelocity = 0f;
            projectileRb.simulated = false; // fully stops physics so it doesn’t keep sliding around
        }

        Destroy(gameObject, 0.6f); // waits a bit before deleting so the animation can finish
    }
}