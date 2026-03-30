using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlPortal : MonoBehaviour, IInteractable
{
    public int levelNumber; // Holds how many gems are needed to enter the level
    public String levelSceneName; // Holds the name of the scene which the portal can bring the player too
    public void Interact()
    {
        if (levelNumber <= GemManager.gemCount) // If replayability of levels is iffy may need to change to ==
        {
            SceneManager.LoadScene(levelSceneName); // Brings to specified scene if player has enough gems
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
