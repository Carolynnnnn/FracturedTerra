using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 500;
    public int currentHealth;
    public float attackCooldown = 0.8f;
    public int attackDamage = 20;

    [Header("Phases")]
    public float phase2Threshold = 0.5f;
    public float phase3Threshold = 0.25f;
    private int currentPhase = 1;

    [Header("References")]
    public Transform player;

	[Header("Projectiles")]
	public GameObject armProjPrefab;
	public GameObject laserPrefab;

	[Header("Spawn")]
	public Vector2 leftArmOffset = new Vector2(-3f, 0f);
	public Vector2 rightArmOffset = new Vector2(3f, 0f);
	public Vector2 leftLaserOffset = new Vector2(-4f, 5f);
	public Vector2 rightLaserOffset = new Vector2(4f, 5f);

    private float attackTimer;
    private Rigidbody2D rb;
	private SpriteRenderer sr;
	private float facingDirection = 1f;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        
        //avoid Boss moving
        rb.constraints = RigidbodyConstraints2D.FreezePosition | 
                         RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        CheckPhase();
        FacePlayer(); //Always set to face player
        
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
	        Debug.Log("Attacking! Phase: " + currentPhase);
            Attack();
            attackTimer = 0f;
        }
    }
    
    void LateUpdate()
    {
	    transform.localScale = new Vector3(facingDirection * 10f, 10f, 1f);
    }

    void FacePlayer()
    {
	    if (player == null) return;
	    facingDirection = player.position.x < transform.position.x ? -1f : 1f;
    }

    void CheckPhase()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        if (healthPercent <= phase3Threshold && currentPhase < 3)
        {
            currentPhase = 3;
            attackCooldown = 0.4f;
            Debug.Log("Boss Phase 3!");
        }
        else if (healthPercent <= phase2Threshold && currentPhase < 2)
        {
            currentPhase = 2;
            attackCooldown = 0.6f;
            Debug.Log("Boss Phase 2!");
        }
    }

    void Attack()
    {
        if (player == null) return;
		Vector2 armOffset = facingDirection > 0 ? rightArmOffset : leftArmOffset;
    	Vector3 origin = transform.position + (Vector3)armOffset;
        
        if (currentPhase == 1)
        {
            // Phase 1: single shooting
            ShootProjectile(armProjPrefab, player.position);
        }
        else if (currentPhase == 2)
        {
            // Phase 2: triple shooting + laser shooting
            ShootProjectile(armProjPrefab, player.position);
            Vector2 dir = (player.position - origin).normalized;
            float angle = 20f;
            ShootProjectileAngle(armProjPrefab, dir, angle);
            ShootProjectileAngle(armProjPrefab, dir, -angle);
			ShootLaser();
        }
        else if (currentPhase == 3)
        {
            // Phase 3: penta shooting + double laser shooting
            ShootProjectile(armProjPrefab, player.position);
            Vector2 dir = (player.position - origin).normalized;
            ShootProjectileAngle(armProjPrefab, dir, 20f);
            ShootProjectileAngle(armProjPrefab, dir, -20f);
            ShootProjectileAngle(armProjPrefab, dir, 40f);
            ShootProjectileAngle(armProjPrefab, dir, -40f);
			ShootLaser();
			//delay the laser shooting time
			Invoke("ShootLaser", 0.5f);
        }
    }

    void ShootProjectile(GameObject prefab, Vector3 target)
    {
		if (prefab == null) return;
    	Vector2 armOffset = facingDirection > 0 ? rightArmOffset : leftArmOffset;
    	Vector3 origin = transform.position + (Vector3)armOffset;
    	GameObject proj = Instantiate(prefab, origin, Quaternion.identity);
    	Vector2 dir = (target - origin).normalized;
    	Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
    	if (projRb != null)
        	projRb.linearVelocity = dir * 5f;
    	Destroy(proj, 3f);
    	}

    void ShootProjectileAngle(GameObject prefab, Vector2 baseDir, float angle)
    {
		if (prefab == null) return;
		Vector2 armOffset = facingDirection > 0 ? rightArmOffset : leftArmOffset;
		Vector3 origin = transform.position + (Vector3)armOffset;
        float rad = angle * Mathf.Deg2Rad;
        Vector2 rotated = new Vector2(
            baseDir.x * Mathf.Cos(rad) - baseDir.y * Mathf.Sin(rad),
            baseDir.x * Mathf.Sin(rad) + baseDir.y * Mathf.Cos(rad)
        );
        GameObject proj = Instantiate(prefab, origin, Quaternion.identity);
        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
    	if (projRb != null)
        	projRb.linearVelocity = rotated * 5f;
    	Destroy(proj, 3f);
    }

	void ShootLaser()
	{
		if (laserPrefab == null || player == null) return;
		Debug.Log("Boss transform.position: " + transform.position);
		Debug.Log("Player position: " + player.position);
		Vector2 laserOffset = facingDirection > 0 ? rightLaserOffset : leftLaserOffset;
		Vector3 origin = transform.position + (Vector3)laserOffset;
    	GameObject laser = Instantiate(laserPrefab, origin, Quaternion.identity);
    	Vector2 dir = (player.position - origin).normalized;
    	LaserProjectile lp = laser.GetComponent<LaserProjectile>();
    	if (lp != null) lp.SetDirection(dir);
		//rotate to player location
    	float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    	laser.transform.rotation = Quaternion.Euler(0, 0, angle);
    	Destroy(laser, 3f);
	}
    public void TakeDamage(int damage)
    {
	    Debug.Log("FinalBoss.TakeDamage called! damage: " + damage);
        currentHealth -= damage;
        Debug.Log("Boss HP: " + currentHealth + "/" + maxHealth);
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Boss defeated! Game complete!");
        GemManager.gemCount++;
		//ensure all attack methods are disabled
		CancelInvoke();
		gameObject.SetActive(false);
    }
}
