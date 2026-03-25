using UnityEngine;
using UnityEngine.UI;

public class SkinControl : MonoBehaviour
{
    [Header("REAL GAME BODY")]
    [SerializeField] private SpriteRenderer baseBodyRenderer;

    [Header("PREVIEW BODY")]
    [SerializeField] private Image previewBodyImage;

    private Color[] skinColors;

    private void Awake()
    {
        skinColors = new Color[5];

        ColorUtility.TryParseHtmlString("#FDDBC7", out skinColors[0]); // pale
        ColorUtility.TryParseHtmlString("#CC9966", out skinColors[1]); // tan
        ColorUtility.TryParseHtmlString("#885533", out skinColors[2]); // brown
        ColorUtility.TryParseHtmlString("#442211", out skinColors[3]); // dark
        ColorUtility.TryParseHtmlString("#819EED", out skinColors[4]); // blue
    }

    private void Start()
    {
        ApplySkinFromData();
    }

    public void SetSkinColorByIndex(int index)
    {
        if (index < 0 || index >= skinColors.Length) return;

        PlayerAppearanceData.Instance.skinColorIndex = index;
        ApplySkinFromData();
    }

    public void ApplySkinFromData()
    {
        int index = PlayerAppearanceData.Instance.skinColorIndex;

        if (baseBodyRenderer != null)
            baseBodyRenderer.color = skinColors[index];

        if (previewBodyImage != null)
            previewBodyImage.color = skinColors[index];
    }
}