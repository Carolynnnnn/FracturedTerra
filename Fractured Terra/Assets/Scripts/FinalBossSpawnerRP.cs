using UnityEngine;

public class FinalBossSpawnerRP : MonoBehaviour
{
    public GameObject bossPrefab; // the boss to spawn
    public Transform spawnPoint; // where the boss appears
    public FinalBossUIRP bossUI; // UI for boss health bar

    private bool hasSpawned = false; // makes sure boss only spawns once

    public void SpawnBoss()
    {
        if (hasSpawned) return; // prevents multiple spawns
        if (bossPrefab == null || spawnPoint == null) return; // safety check

        GameObject bossObj = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity); // spawns boss in scene

        FinalBossHealthRP bossHealth = bossObj.GetComponent<FinalBossHealthRP>();
        if (bossHealth != null)
        {
            bossHealth.bossUI = bossUI; // links boss to UI so health bar works
        }

        hasSpawned = true; // marks boss as spawned
    }
}