using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISelectableItem : MonoBehaviour
{
    [Header("Item Info")]
    public string itemName;
    public string itemDescription;

    [Header("UI")]
    public Image highlightImage; // drag your selectBox here (optional)

    private UISelectionManager manager;

    private void Start()
    {
        manager = FindObjectOfType<UISelectionManager>();

        if (highlightImage != null)
            highlightImage.enabled = false;
    }

    public void OnClick()
    {
        manager.SelectItem(this);
    }

    public void SetSelected(bool selected)
    {
        if (highlightImage != null)
            highlightImage.enabled = selected;
    }
}