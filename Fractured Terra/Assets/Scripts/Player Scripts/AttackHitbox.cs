using UnityEngine;

public class AttackHitbox : MonoBehaviour //Detects enemies hit by the player's attack

{
    
    [SerializeField] private int damage = 1; //Damage given to enemies

    private void OnTriggerEnter2D(Collider2D collision)
{
    EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

    if (enemy != null)
    {
        enemy.TakeDamage(damage); //Deal damage
        Debug.Log("Enemy hit!"); 
        
    }
}

}
