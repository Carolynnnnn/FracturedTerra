using UnityEngine;

public class SlimeAI : MonoBehaviour
{ // Slimes attack by simply touching the player
    public float moveSpeed = 3f;
    public float detectionRange = 9f; // When the slime will start chasing the player
    
    private Transform player;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Get player
    }
    void Update()
    {
        if (player == null) return; // Does nothing if there is no player

        float distance = Vector2.Distance(transform.position, player.position); // Distance from player

        if (distance <= detectionRange) // If player in detection range
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        }
    }
}
