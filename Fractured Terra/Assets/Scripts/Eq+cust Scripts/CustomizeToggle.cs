using System.Collections;
using UnityEngine;

public class CustomizeToggle : MonoBehaviour
{
    public GameObject customizePanel;
    public RectTransform customizeRect;
    public RectTransform customizeButtonRect;
    public CanvasGroup canvasGroup;
    public Canvas canvas;

    public float animationDuration = 0.25f;

    private bool isOpen = false;
    private bool isAnimating = false;

    private Vector2 openPosition = Vector2.zero;

    void Start()
    {
        customizePanel.SetActive(false);
        canvasGroup.alpha = 0f;
        customizeRect.localScale = Vector3.zero;
        customizeRect.anchoredPosition = GetButtonPositionInPanelSpace();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isAnimating)
        {
            ToggleCustomize();
        }
    }

    public void ToggleCustomize()
    {
        if (isAnimating) return;

        if (isOpen)
            StartCoroutine(CloseCustomize());
        else
            StartCoroutine(OpenCustomize());
    }

    Vector2 GetButtonPositionInPanelSpace()
    {
        RectTransform panelParent = customizeRect.parent as RectTransform;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            customizeButtonRect.position
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

    IEnumerator OpenCustomize()
    {
        isAnimating = true;

        Vector2 closedPosition = GetButtonPositionInPanelSpace();

        customizePanel.SetActive(true);
        customizeRect.anchoredPosition = closedPosition;
        customizeRect.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;

        float time = 0f;

        while (time < animationDuration)
        {
            float t = time / animationDuration;

            customizeRect.anchoredPosition = Vector2.Lerp(closedPosition, openPosition, t);
            customizeRect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            time += Time.deltaTime;
            yield return null;
        }

        customizeRect.anchoredPosition = openPosition;
        customizeRect.localScale = Vector3.one;
        canvasGroup.alpha = 1f;

        isOpen = true;
        isAnimating = false;
    }

    IEnumerator CloseCustomize()
    {
        isAnimating = true;

        Vector2 closedPosition = GetButtonPositionInPanelSpace();

        float time = 0f;

        while (time < animationDuration)
        {
            float t = time / animationDuration;

            customizeRect.anchoredPosition = Vector2.Lerp(openPosition, closedPosition, t);
            customizeRect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

            time += Time.deltaTime;
            yield return null;
        }

        customizeRect.anchoredPosition = closedPosition;
        customizeRect.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;
        customizePanel.SetActive(false);

        isOpen = false;
        isAnimating = false;
    }
}