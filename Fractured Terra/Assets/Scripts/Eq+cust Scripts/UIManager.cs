using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Left Display Slots")]
    public Image equippedDisplayImage;
    public Image abilityDisplayImage;

    [Header("Description")]
    public TMP_Text descriptionText;

    private UISlotItem currentSelected;

    public void SelectSlot(UISlotItem slot)
    {
        currentSelected = slot;

        // update description
        descriptionText.text = slot.itemName + "\n\n" + slot.description;

        // update left side display
        if (slot.isEquipment)
        {
            equippedDisplayImage.sprite = slot.icon;
            equippedDisplayImage.enabled = true;
        }
        else
        {
            abilityDisplayImage.sprite = slot.icon;
            abilityDisplayImage.enabled = true;
        }
    }
}