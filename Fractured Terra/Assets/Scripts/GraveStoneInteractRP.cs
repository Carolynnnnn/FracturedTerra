using UnityEngine;

public class GravestoneInteractRP : MonoBehaviour
{
    public InfiniteArenaSpawnerRP spawner;

    private bool playerInRange = false;

    void Update()
    {
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
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}