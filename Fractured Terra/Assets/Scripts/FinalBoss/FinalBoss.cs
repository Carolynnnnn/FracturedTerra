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
    public GameObject projectilePrefab;

    private float attackTimer;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
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

    void FacePlayer()
    {
        if (player == null) return;
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1); // face left
        else
            transform.localScale = new Vector3(1, 1, 1);  // face right
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
        if (player == null || projectilePrefab == null) return;
        
        if (currentPhase == 1)
        {
            // Phase 1: single shooting
            ShootProjectile(player.position);
        }
        else if (currentPhase == 2)
        {
            // Phase 2: triple shooting
            ShootProjectile(player.position);
            Vector2 dir = (player.position - transform.position).normalized;
            float angle = 20f;
            ShootProjectileAngle(dir, angle);
            ShootProjectileAngle(dir, -angle);
        }
        else if (currentPhase == 3)
        {
            // Phase 3: penta shooting
            ShootProjectile(player.position);
            Vector2 dir = (player.position - transform.position).normalized;
            ShootProjectileAngle(dir, 20f);
            ShootProjectileAngle(dir, -20f);
            ShootProjectileAngle(dir, 40f);
            ShootProjectileAngle(dir, -40f);
        }
    }

    void ShootProjectile(Vector3 target)
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 dir = (target - transform.position).normalized;
        proj.GetComponent<Rigidbody2D>().linearVelocity = dir * 5f;
    }

    void ShootProjectileAngle(Vector2 baseDir, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 rotated = new Vector2(
            baseDir.x * Mathf.Cos(rad) - baseDir.y * Mathf.Sin(rad),
            baseDir.x * Mathf.Sin(rad) + baseDir.y * Mathf.Cos(rad)
        );
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().linearVelocity = rotated * 5f;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss HP: " + currentHealth + "/" + maxHealth);
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Boss defeated! Game complete!");
        GemManager.gemCount++;
        Invoke("ReturnToHub", 2f);
    }
}
