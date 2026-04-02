using UnityEngine;

public class AbilityProjectileRP : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 2f;
    public int damage = 1;
    public LayerMask enemyLayer;
    public LayerMask breakableLayer;

    private Vector2 direction = Vector2.right;

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
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            EnemyHealthRP enemy = other.GetComponent<EnemyHealthRP>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        if (((1 << other.gameObject.layer) & breakableLayer) != 0)
        {
            if (other.CompareTag("breakitem"))
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}