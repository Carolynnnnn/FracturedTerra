using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
			
			Vector2 armOff = facingDirection > 0 ? rightArmOffset : leftArmOffset;
			Vector3 laserOrigin = transform.position + (Vector3)armOff;
			Debug.Log("First laser origin: " + laserOrigin);
			ShootLaserAt(laserOrigin);
			//delay the laser shooting time
			StartCoroutine(DelayedLaser(laserOrigin, 0.5f));
        }
    }

    void ShootProjectile(GameObject prefab, Vector3 target)
    {
		if (prefab == null) return;
    	Vector2 armOffset = facingDirection > 0 ? rightArmOffset : leftArmOffset;
    	Vector3 origin = transform.position + (Vector3)armOffset;
	    Debug.Log("ShootProjectile origin: " + origin);
    	GameObject proj = Instantiate(prefab, origin, Quaternion.identity);
    	Vector2 dir = (target - origin).normalized;
    	Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
    	if (projRb != null)
        	projRb.linearVelocity = dir * 3f;
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
        	projRb.linearVelocity = rotated * 3f;
    	Destroy(proj, 3f);
    }

	void ShootLaser()
	{
    	Vector2 armOffset = facingDirection > 0 ? rightArmOffset : leftArmOffset;
    	Vector3 origin = transform.position + (Vector3)armOffset;
	    Debug.Log("ShootLaser origin: " + origin);
    	ShootLaserAt(origin);
	}

	void ShootLaserAt(Vector3 origin)
	{
    	if (laserPrefab == null || player == null) return;
    	GameObject laser = Instantiate(laserPrefab, origin, Quaternion.identity);
    	Vector2 dir = (player.position - origin).normalized;
    	float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    	laser.transform.rotation = Quaternion.Euler(0, 0, angle);
    	Destroy(laser, 3f);
	}
    
	IEnumerator DelayedLaser(Vector3 origin, float delay)
	{
    	yield return new WaitForSeconds(delay);
	    Debug.Log("DelayedLaser origin: " + origin);
    	ShootLaserAt(origin);
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
        GameObject gemChest = GameObject.Find("GemChest");
        BossSceneManager bsm = FindObjectOfType<BossSceneManager>();
        if (bsm != null) bsm.OnBossDefeated();
		//ensure all attack methods are disabled
		CancelInvoke();
		gameObject.SetActive(false);
    }
}
