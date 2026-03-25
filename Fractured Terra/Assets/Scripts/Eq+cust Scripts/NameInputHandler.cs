using UnityEngine;
using TMPro;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        if (PlayerNameData.Instance != null && inputField != null)
        {
            inputField.text = PlayerNameData.Instance.playerName;
        }
    }

    public void OnNameChanged()
    {
        if (PlayerNameData.Instance != null && inputField != null)
        {
            PlayerNameData.Instance.playerName = inputField.text;
            Debug.Log("Player name set to: " + PlayerNameData.Instance.playerName);
        }
    }
}