using UnityEngine;
using UnityEngine.UI;

public class UISlotItem : MonoBehaviour
{
    [Header("Item Info")]
    public string itemName;
    public string description;

    [Header("Visual")]
    public Sprite icon;

    [Header("Type")]
    public bool isEquipment; // true = equipment, false = ability

    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void OnClick()
    {
        uiManager.SelectSlot(this);
    }
}