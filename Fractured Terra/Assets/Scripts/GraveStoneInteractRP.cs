using UnityEngine;

public class GravestoneInteractRP : MonoBehaviour
{
    public InfiniteArenaSpawnerRP spawner; // reference to arena spawner

    private bool playerInRange = false; // tracks if player is close enough to interact

    void Update()
    {
        // if player is near and presses P, stop the arena spawning (basically an exit/stop mechanic)
        if (playerInRange && Input.GetKeyDown(KeyCode.P))
        {
            spawner.StopSpawning();
            Debug.Log("Spawner stopped by gravestone");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // player entered interaction zone
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // player left interaction zone
        }
    }
}