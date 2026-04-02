using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlPortal : MonoBehaviour, IInteractable
{
    public int levelNumber; // Holds how many gems are needed to enter the level
    public String levelSceneName; // Holds the name of the scene which the portal can bring the player too
    public void Interact()
    {
        if (levelNumber == GemManager.gemCount) // Replayability of levels may be complicated, player must have exact gem count
        {
            SceneManager.LoadScene(levelSceneName); // Brings to specified scene if player has enough gems
        }
        else Debug.Log("This portal requires exactly " + levelNumber + " gems");
    }

    public bool CanInteract()
    {
        return true;
    }
}
