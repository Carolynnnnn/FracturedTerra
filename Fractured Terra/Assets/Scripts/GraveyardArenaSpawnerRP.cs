using System.Collections;
using UnityEngine;

public class GraveyardArenaSpawnerRP : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public int totalEnemies = 7;
    public float spawnDelay = 2f;

    private bool started = false;

    public void StartSpawning()
    {
        if (started) return;

        started = true;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}