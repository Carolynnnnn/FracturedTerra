using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonsController : MonoBehaviour
{
    public void Play()
    {
        //SceneManager.LoadScene("HubWorld");
    }

    public void Quit()
    {
        Application.Quit();
    }
}