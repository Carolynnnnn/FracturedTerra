using UnityEngine;

public class PlayerDefenseRP : MonoBehaviour
{
    public bool isProtected = false;

    public void SetProtected(bool value)
    {
        isProtected = value;
    }
}