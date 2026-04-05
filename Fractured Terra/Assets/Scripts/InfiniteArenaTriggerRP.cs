using UnityEngine;

public class InfiniteArenaTriggerRP : MonoBehaviour
{
    public InfiniteArenaSpawnerRP spawner;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            spawner.StartSpawning();
        }
    }
}