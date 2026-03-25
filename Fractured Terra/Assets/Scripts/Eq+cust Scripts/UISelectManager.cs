using UnityEngine;
using TMPro;

public class UISelectionManager : MonoBehaviour
{
    public TMP_Text descriptionText;

    private UISelectableItem currentSelected;

    public void SelectItem(UISelectableItem item)
    {
        if (currentSelected != null)
            currentSelected.SetSelected(false);

        currentSelected = item;
        currentSelected.SetSelected(true);

        UpdateDescription(item);
    }

    void UpdateDescription(UISelectableItem item)
    {
        if (descriptionText != null)
        {
            descriptionText.text = item.itemName + "\n\n" + item.itemDescription;
        }
    }
}