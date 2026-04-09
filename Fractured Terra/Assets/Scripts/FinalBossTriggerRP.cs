using UnityEngine;

public class FinalBossTriggerRP : MonoBehaviour
{
    public FinalBossSpawnerRP bossSpawner; // reference to the boss spawner

    private bool triggered = false; // makes sure boss only spawns once

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return; // prevents re-triggering

        if (other.CompareTag("Player"))
        {
            triggered = true; // locks it
            bossSpawner.SpawnBoss(); // spawns boss when player enters area
        }
    }
}