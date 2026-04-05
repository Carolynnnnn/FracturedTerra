using System.Collections;
using UnityEngine;

public class InfiniteArenaSpawnerRP : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnDelay = 2f;

    private bool hasStarted = false;
    private bool stopSpawning = false;

    public void StartSpawning()
    {
        if (hasStarted) return;

        hasStarted = true;
        StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }

    IEnumerator SpawnRoutine()
    {
        while (!stopSpawning)
        {
            if (enemyPrefab != null && spawnPoints.Length > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}