using UnityEngine;
using UnityEngine.UI;

public class HairControl : MonoBehaviour
{
    [Header("REAL GAME HAIR")]
    public SpriteRenderer shortHair;
    public SpriteRenderer longHair;

    [Header("PREVIEW HAIR")]
    public Image previewHair;
    public RectTransform previewHairRect;

    [Header("PREVIEW SPRITES")]
    public Sprite shortHairPreviewSprite;
    public Sprite longHairPreviewSprite;

    [Header("PREVIEW POSITIONS")]
    public Vector2 shortHairPreviewPosition = new Vector2(0f, 0f);
    public Vector2 longHairPreviewPosition = new Vector2(0f, 0f);

    private Color[] hairColors;

    private void Start()
    {
        hairColors = new Color[5];

        ColorUtility.TryParseHtmlString("#C78235", out hairColors[0]);
        ColorUtility.TryParseHtmlString("#DCBD92", out hairColors[1]);
        ColorUtility.TryParseHtmlString("#805A40", out hairColors[2]);
        ColorUtility.TryParseHtmlString("#000000", out hairColors[3]);
        ColorUtility.TryParseHtmlString("#FF8DA1", out hairColors[4]);

        ApplyHairFromData();
    }

    public void SetShortHair()
    {
        PlayerAppearanceData.Instance.isShortHair = true;
        ApplyHairFromData();
    }

    public void SetLongHair()
    {
        PlayerAppearanceData.Instance.isShortHair = false;
        ApplyHairFromData();
    }

    public void SetHairColorByIndex(int index)
    {
        if (index < 0 || index >= hairColors.Length) return;

        PlayerAppearanceData.Instance.hairColorIndex = index;
        ApplyHairFromData();
    }

    public void ApplyHairFromData()
    {
        bool isShort = PlayerAppearanceData.Instance.isShortHair;
        int colorIndex = PlayerAppearanceData.Instance.hairColorIndex;

        if (shortHair != null) shortHair.enabled = isShort;
        if (longHair != null) longHair.enabled = !isShort;

        if (shortHair != null) shortHair.color = hairColors[colorIndex];
        if (longHair != null) longHair.color = hairColors[colorIndex];

        if (previewHair != null)
        {
            previewHair.sprite = isShort ? shortHairPreviewSprite : longHairPreviewSprite;
            previewHair.color = hairColors[colorIndex];
        }

        if (previewHairRect != null)
        {
            previewHairRect.anchoredPosition = isShort
                ? shortHairPreviewPosition
                : longHairPreviewPosition;
        }
    }
}