using UnityEngine;

public class InfiniteArenaTriggerRP : MonoBehaviour
{
    public InfiniteArenaSpawnerRP spawner; // reference to infinite spawner

    private bool triggered = false; // makes sure it only starts once

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return; // prevents retriggering

        if (other.CompareTag("Player"))
        {
            triggered = true; // locks it
            spawner.StartSpawning(); // starts infinite enemy waves when player enters area
        }
    }
}