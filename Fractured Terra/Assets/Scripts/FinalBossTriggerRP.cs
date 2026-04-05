using UnityEngine;

public class FinalBossTriggerRP : MonoBehaviour
{
    public FinalBossSpawnerRP bossSpawner;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            bossSpawner.SpawnBoss();
        }
    }
}