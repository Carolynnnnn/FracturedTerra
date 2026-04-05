using UnityEngine;

public class Level2GemReset : MonoBehaviour
{
    private static bool _initialized;

    private void Awake()
    {
        // Reset the guard when the scene loads fresh
        _initialized = false;
    }

    private void Start()
    {
        if (_initialized) return;
        _initialized = true;
        GemManager.gemCount = 2;
        Debug.Log("[Level2GemReset] gemCount set to 2");
    }
}
