using UnityEngine;

public class ArenaTriggerRP : MonoBehaviour
{
    public GraveyardArenaSpawnerRP spawner; // reference to the spawner that starts the fight

    private bool triggered = false; // makes sure it only triggers once

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return; // prevents re-triggering if player walks in again

        if (other.CompareTag("Player"))
        {
            triggered = true; // locks it so it can’t happen again
            spawner.StartSpawning(); // starts enemy waves / arena fight
        }
    }
}