using UnityEngine;
public class LaserProjectile : MonoBehaviour
{
    public int damage = 15;
    public float lifetime = 3f;
    public float speed = 8f;
    private Vector2 moveDirection;

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth ph = other.GetComponentInParent<PlayerHealth>();
    	if (ph != null)
    	{
        	ph.TakeDamage(damage);
        	Destroy(gameObject);
    	}
	}
}