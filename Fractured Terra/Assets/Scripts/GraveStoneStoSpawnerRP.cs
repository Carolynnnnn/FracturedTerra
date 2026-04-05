using UnityEngine;

public class GravestoneStopSpawnerRP : MonoBehaviour, IInteractable
{
    public InfiniteArenaSpawnerRP spawner;

    private bool hasBeenUsed = false;

    public bool CanInteract()
    {
        return !hasBeenUsed;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        hasBeenUsed = true;

        if (spawner != null)
        {
            spawner.StopSpawning();
            Debug.Log("Gravestone used. Enemy spawning stopped.");
        }
    }
}