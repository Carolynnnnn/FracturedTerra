using UnityEngine;

public class Level2GemReset : MonoBehaviour
{
    private void Awake()
    {
        GemManager.gemCount = 0;
    }
}
