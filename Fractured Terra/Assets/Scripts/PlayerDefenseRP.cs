using UnityEngine;

public class PlayerDefenseRP : MonoBehaviour
{
    public bool isProtected = false; // tracks if player is currently protected (used by shield ability)

    public void SetProtected(bool value)
    {
        isProtected = value; // turns protection on/off (prevents damage in other scripts)
    }
}