using UnityEngine;

public class InventorySelect : MonoBehaviour
{
    public GameObject[] slots;
    public int selectedIndex = 0;

    private int columns = 3;
    private int totalSlots = 12;

    public InventoryManager inventoryManager;

    void Start()
    {
        UpdateSelectionHighlight();
        NotifyManager();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }

    void MoveRight()
    {
        if ((selectedIndex % columns) < columns - 1 && selectedIndex + 1 < totalSlots)
        {
            selectedIndex++;
            UpdateSelectionHighlight();
            NotifyManager();
        }
    }

    void MoveLeft()
    {
        if ((selectedIndex % columns) > 0)
        {
            selectedIndex--;
            UpdateSelectionHighlight();
            NotifyManager();
        }
    }

    void MoveUp()
    {
        if (selectedIndex - columns >= 0)
        {
            selectedIndex -= columns;
            UpdateSelectionHighlight();
            NotifyManager();
        }
    }

    void MoveDown()
    {
        if (selectedIndex + columns < totalSlots)
        {
            selectedIndex += columns;
            UpdateSelectionHighlight();
            NotifyManager();
        }
    }

    void UpdateSelectionHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Transform highlight = slots[i].transform.Find("SelectionHighlight");

            if (highlight != null)
            {
                highlight.gameObject.SetActive(i == selectedIndex);
            }
        }
    }

    void NotifyManager()
    {
        if (inventoryManager != null)
        {
            inventoryManager.SelectSlot(selectedIndex);
        }
    }
}