using System.Collections;
using UnityEngine;

public class GraveyardArenaSpawnerRP : MonoBehaviour
{
    public GameObject enemyPrefab; // enemy to spawn
    public Transform[] spawnPoints; // possible spawn locations

    public int totalEnemies = 7; // total enemies in this arena wave
    public float spawnDelay = 2f; // time between each spawn

    private bool started = false; // makes sure it only starts once

    public void StartSpawning()
    {
        if (started) return; // prevents restarting

        started = true;
        StartCoroutine(SpawnRoutine()); // begins spawning enemies
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            int index = Random.Range(0, spawnPoints.Length); // picks a random spawn point

            Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity); // spawns enemy

            yield return new WaitForSeconds(spawnDelay); // waits before next spawn
        }
    }
}