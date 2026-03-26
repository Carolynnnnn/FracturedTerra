using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 1; // Starting enemy health

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " Took an attack. Health left: " + health);

        if (health <= 0) // if enemy's health reaches 0
        {
            Destroy(gameObject); // removes enemy from the scene
        }
    }
}