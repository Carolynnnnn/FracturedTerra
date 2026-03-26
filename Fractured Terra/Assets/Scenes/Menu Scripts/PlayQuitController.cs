using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonsController : MonoBehaviour
{
    public void Play()
    { 
        //SceneManager.LoadScene("Carylions Hubworld Backup (Scene Asset)");
    }

    public void Quit()
    {
        Application.Quit();
    }
}