using UnityEngine;

public class ArmProjectile : MonoBehaviour
{
    public float damage = 20f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log("ArmProjectile hit: " + other.gameObject.name + " tag: " + other.tag);
        PlayerHealth ph = other.GetComponentInParent<PlayerHealth>();
    	if (ph != null)
    	{
        	ph.TakeDamage(damage);
        	Destroy(gameObject);
    	}
	}
}