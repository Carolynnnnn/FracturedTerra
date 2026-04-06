using System.Collections;
using UnityEngine;

public class InfiniteArenaSpawnerRP : MonoBehaviour
{
    public GameObject enemyPrefab; // enemy to spawn
    public Transform[] spawnPoints; // possible spawn locations
    public float spawnDelay = 2f; // time between spawns

    private bool hasStarted = false; // makes sure it only starts once
    private bool stopSpawning = false; // controls when to stop spawning

    public void StartSpawning()
    {
        if (hasStarted) return; // prevents restarting

        hasStarted = true;
        StartCoroutine(SpawnRoutine()); // starts infinite spawning loop
    }

    public void StopSpawning()
    {
        stopSpawning = true; // stops the loop (used by gravestone / interaction)
    }

    IEnumerator SpawnRoutine()
    {
        while (!stopSpawning) // keeps spawning until stopped
        {
            if (enemyPrefab != null && spawnPoints.Length > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length); // picks random spawn point
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity); // spawns enemy
            }

            yield return new WaitForSeconds(spawnDelay); // wait before next spawn
        }
    }
}