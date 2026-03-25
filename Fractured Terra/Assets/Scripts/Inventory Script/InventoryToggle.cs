using System.Collections;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel;
    public RectTransform inventoryRect;
    public RectTransform inventoryButtonRect;
    public CanvasGroup canvasGroup;
    public Canvas canvas;

    public float animationDuration = 0.25f;

    private bool isOpen = false;
    private bool isAnimating = false;

    private Vector2 openPosition = Vector2.zero;

    void Start()
    {
        inventoryPanel.SetActive(false);
        canvasGroup.alpha = 0f;
        inventoryRect.localScale = Vector3.zero;
        inventoryRect.anchoredPosition = GetButtonPositionInPanelSpace();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !isAnimating)
        {
            ToggleInventory();
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public void ToggleInventory()
    {
        if (isAnimating) return;

        if (isOpen)
            StartCoroutine(CloseInventory());
        else
            StartCoroutine(OpenInventory());
    }

    Vector2 GetButtonPositionInPanelSpace()
    {
        RectTransform panelParent = inventoryRect.parent as RectTransform;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            inventoryButtonRect.position
        );

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelParent,
            screenPoint,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPoint
        );

        return localPoint;
    }

    IEnumerator OpenInventory()
    {
        isAnimating = true;

        Vector2 closedPosition = GetButtonPositionInPanelSpace();

        inventoryPanel.SetActive(true);
        inventoryRect.anchoredPosition = closedPosition;
        inventoryRect.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;

        float time = 0f;

        while (time < animationDuration)
        {
            float t = time / animationDuration;

            inventoryRect.anchoredPosition = Vector2.Lerp(closedPosition, openPosition, t);
            inventoryRect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            time += Time.deltaTime;
            yield return null;
        }

        inventoryRect.anchoredPosition = openPosition;
        inventoryRect.localScale = Vector3.one;
        canvasGroup.alpha = 1f;

        isOpen = true;
        isAnimating = false;
    }

    IEnumerator CloseInventory()
    {
        isAnimating = true;

        Vector2 closedPosition = GetButtonPositionInPanelSpace();

        float time = 0f;

        while (time < animationDuration)
        {
            float t = time / animationDuration;

            inventoryRect.anchoredPosition = Vector2.Lerp(openPosition, closedPosition, t);
            inventoryRect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

            time += Time.deltaTime;
            yield return null;
        }

        inventoryRect.anchoredPosition = closedPosition;
        inventoryRect.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;
        inventoryPanel.SetActive(false);

        isOpen = false;
        isAnimating = false;
    }
}