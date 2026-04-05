using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Portal : MonoBehaviour, IInteractable
{
    public int levelNumber;
    public string levelSceneName;

    public void Interact()
    {
        if (levelNumber == GemManager.gemCount)
        {
            AbilityUnlockManagerRP.Instance.UnlockAbility(10);
            SceneManager.LoadScene(levelSceneName);
        }
        else Debug.Log("This portal requires exactly " + levelNumber + " gems");
    }

    public bool CanInteract()
    {
        return true;
    }
}
