using UnityEngine;

public class BreakablePotScript : MonoBehaviour
{
    public GameObject breakEffect; // optional particle or sprite
    public AudioClip breakSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player attacked it
        if (collision.gameObject.CompareTag("Player"))
        {
            BreakPot();
        }
    }

    void BreakPot()
    {
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }

        Destroy(gameObject);
    }
}