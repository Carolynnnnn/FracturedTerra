using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int maxLife;
    public int currentLife;
    public bool canUse;
    public GameObject worldPrefab;

    public InventoryItem(string name, string desc, Sprite sprite, int life, bool usable, GameObject prefab)
    {
        itemName = name;
        description = desc;
        icon = sprite;
        maxLife = life;
        currentLife = life;
        canUse = usable;
        worldPrefab = prefab;
    }
}

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    public static List<InventoryItem> items = new List<InventoryItem>(); // Made static to account for scene change
    public int maxSlots = 12;

    [Header("Slot UI")]
    public GameObject[] slots;

    [Header("Right Side UI")]
    public Image itemView;
    public TMP_Text itemDescriptionText;
    public TMP_Text productLifeText;

    [Header("Optional")]
    public InventoryToggle inventoryToggle;

    [Header("Drop Settings")]
    public Transform playerTransform;
    public Vector3 dropOffset = new Vector3(1f, 0f, 0f);

    private int selectedIndex = 0;
    private float holdTimer = 0f;

    void Start()
    {
        RefreshSlots();
        UpdateRightPanel();
    }

    void Update()
    {
        HandleDropHold();
    }

    public void SelectSlot(int index)
    {
        selectedIndex = index;
        UpdateRightPanel();
    }

    public void AddItem(InventoryItem item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventory full");
            return;
        }

        items.Add(item);
        RefreshSlots();
        UpdateRightPanel();
    }

    public void UseSelectedItem()
    {
        if (selectedIndex < 0 || selectedIndex >= items.Count)
        {
            Debug.Log("No item in selected slot.");
            return;
        }

        InventoryItem item = items[selectedIndex];

        if (!item.canUse)
        {
            Debug.Log(item.itemName + " cannot be used.");
            return;
        }

        item.currentLife--;
        Debug.Log("Used " + item.itemName);

        if (item.currentLife <= 0)
        {
            items.RemoveAt(selectedIndex);

            if (selectedIndex >= items.Count)
                selectedIndex = Mathf.Max(0, items.Count - 1);
        }

        RefreshSlots();
        UpdateRightPanel();
    }

    void HandleDropHold()
    {
        if (inventoryToggle == null) return;
        if (!inventoryToggle.IsOpen()) return;

        if (selectedIndex < 0 || selectedIndex >= items.Count)
        {
            holdTimer = 0f;
            return;
        }

        if (Input.GetKey(KeyCode.L))
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= 3f)
            {
                DropSelectedItem();
                holdTimer = 0f;
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }

    public void DropSelectedItem()
    {
        if (selectedIndex < 0 || selectedIndex >= items.Count) return;

        InventoryItem item = items[selectedIndex];

        if (item.worldPrefab != null && playerTransform != null)
        {
            Instantiate(item.worldPrefab, playerTransform.position + dropOffset, Quaternion.identity);
        }

        Debug.Log("Dropped " + item.itemName);
        items.RemoveAt(selectedIndex);

        if (selectedIndex >= items.Count)
            selectedIndex = Mathf.Max(0, items.Count - 1);

        RefreshSlots();
        UpdateRightPanel();
    }

    void RefreshSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Transform iconTransform = slots[i].transform.Find("ItemIcon");

            if (iconTransform == null) continue;

            Image iconImage = iconTransform.GetComponent<Image>();

            if (iconImage == null) continue;

            if (i < items.Count)
            {
                iconImage.sprite = items[i].icon;
                iconImage.enabled = true;
                iconImage.color = Color.white;
            }
            else
            {
                iconImage.sprite = null;
                iconImage.enabled = false;
            }
        }
    }

    void UpdateRightPanel()
    {
        if (selectedIndex < 0 || selectedIndex >= items.Count)
        {
            if (itemView != null)
            {
                itemView.sprite = null;
                itemView.enabled = false;
            }

            if (itemDescriptionText != null)
                itemDescriptionText.text = "No item selected";

            if (productLifeText != null)
                productLifeText.text = "Life: -";

            return;
        }

        InventoryItem item = items[selectedIndex];

        if (itemView != null)
        {
            itemView.enabled = true;
            itemView.sprite = item.icon;
        }

        if (itemDescriptionText != null)
            itemDescriptionText.text = item.itemName + "\n\n" + item.description;

        if (productLifeText != null)
            productLifeText.text = "Life: " + item.currentLife + "/" + item.maxLife;
    }
    
    // Additions for NPC quests
    public InventoryItem FindItemByName(string itemName) // Finds an item in the inventory based on its name
    {
        return items.Find(i => i.itemName == itemName);
    }
    public bool RemoveItemByName(string itemName) // Removes an item based on its name
    {
        InventoryItem item = FindItemByName(itemName);
        if (item != null) // Remove item and return true
        { 
            items.Remove(item);
            RefreshSlots();
            UpdateRightPanel();
            return true;
        }
        return false;
    }
}