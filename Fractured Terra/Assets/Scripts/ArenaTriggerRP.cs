using UnityEngine;

public class ArenaTriggerRP : MonoBehaviour
{
    public GraveyardArenaSpawnerRP spawner;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            spawner.StartSpawning();
        }
    }
}