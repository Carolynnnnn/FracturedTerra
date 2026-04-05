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
		GetComponent<Collider2D>().enabled = false;
    	Invoke("EnableCollider", 0.3f);	
        Destroy(gameObject, lifetime);
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