using UnityEngine;

public class PlayerNameData : MonoBehaviour
{
    public static PlayerNameData Instance;

    public string playerName = "Player";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}