using UnityEngine;

public class GravestoneStopSpawnerRP : MonoBehaviour, IInteractable
{
    public InfiniteArenaSpawnerRP spawner; // reference to arena spawner

    private bool hasBeenUsed = false; // makes sure it can only be used once

    public bool CanInteract()
    {
        return !hasBeenUsed; // only interact if not already used
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        hasBeenUsed = true; // locks it after use

        if (spawner != null)
        {
            spawner.StopSpawning(); // stops infinite enemy waves (used as a “quit arena” option)
            Debug.Log("Gravestone used. Enemy spawning stopped.");
        }
    }
}