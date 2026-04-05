using UnityEngine;

public class FinalBossSpawnerRP : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnPoint;
    public FinalBossUIRP bossUI;

    private bool hasSpawned = false;

    public void SpawnBoss()
    {
        if (hasSpawned) return;
        if (bossPrefab == null || spawnPoint == null) return;

        GameObject bossObj = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);

        FinalBossHealthRP bossHealth = bossObj.GetComponent<FinalBossHealthRP>();
        if (bossHealth != null)
        {
            bossHealth.bossUI = bossUI;
        }

        hasSpawned = true;
    }
}