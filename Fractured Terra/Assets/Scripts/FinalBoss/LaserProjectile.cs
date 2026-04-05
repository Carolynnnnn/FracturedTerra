using UnityEngine;
public class LaserProjectile : MonoBehaviour
{
    public int damage = 15;
    public float lifetime = 3f;
    public float speed = 8f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}