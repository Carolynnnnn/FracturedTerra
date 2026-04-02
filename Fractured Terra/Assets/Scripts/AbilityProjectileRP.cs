using UnityEngine;

public class AbilityProjectileRP : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 0.6f;
    public int damage = 1;
    public LayerMask enemyLayer;
    public LayerMask breakableLayer;

    private Vector2 direction = Vector2.right;
    private bool hasHitSomething = false;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (hasHitSomething) return;

        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHitSomething) return;

        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            EnemyHealthRP enemy = other.GetComponent<EnemyHealthRP>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            hasHitSomething = true;
            Destroy(gameObject);
            return;
        }

        if (((1 << other.gameObject.layer) & breakableLayer) != 0)
        {
            if (other.CompareTag("breakitem"))
            {
                Destroy(other.gameObject);
            }

            hasHitSomething = true;
            Destroy(gameObject);
        }
    }
}