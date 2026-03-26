using UnityEngine;
using UnityEngine.UI;

public class StrengthBarUI : MonoBehaviour
{
    [Header("Strength Values")]
    public float currentStrength = 75f;
    public float maxStrength = 100f;

    [Header("UI References")]
    public Image strengthFill;

    void Start()
    {
        UpdateBar();
    }

    void Update()
    {
        // temporary testing
        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            currentStrength += 10f;
            currentStrength = Mathf.Clamp(currentStrength, 0f, maxStrength);
            UpdateBar();
        }

        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            currentStrength -= 10f;
            currentStrength = Mathf.Clamp(currentStrength, 0f, maxStrength);
            UpdateBar();
        }
    }

    public void SetStrength(float value)
    {
        currentStrength = Mathf.Clamp(value, 0f, maxStrength);
        UpdateBar();
    }

    void UpdateBar()
    {
        if (strengthFill != null)
        {
            strengthFill.fillAmount = currentStrength / maxStrength;
        }
    }
}