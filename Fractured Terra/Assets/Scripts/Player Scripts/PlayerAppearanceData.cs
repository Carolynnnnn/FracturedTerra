using UnityEngine;

public class PlayerAppearanceData : MonoBehaviour
{
    public static PlayerAppearanceData Instance;

    [Header("Saved Appearance")]
    public bool isShortHair = true;
    public int hairColorIndex = 0;
    public int skinColorIndex = 0;

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