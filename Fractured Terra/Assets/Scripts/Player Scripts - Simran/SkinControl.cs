using UnityEngine;

public class SkinControl : MonoBehaviour
{
    [SerializeField] private SpriteRenderer baseBodyRenderer;

    private Color[] skinColors;
    private int currentIndex = 0;

    private void Awake()
    {
        skinColors = new Color[5];

        ColorUtility.TryParseHtmlString("#FDDBC7", out skinColors[0]); // pale
        ColorUtility.TryParseHtmlString("#CC9966", out skinColors[1]); // tan
        ColorUtility.TryParseHtmlString("#885533", out skinColors[2]); // brown
        ColorUtility.TryParseHtmlString("#442211", out skinColors[3]); // black
        ColorUtility.TryParseHtmlString("#819eed", out skinColors[4]); // blue
    }

    void Start()
    {
        ApplyColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentIndex = (currentIndex + 1) % skinColors.Length;
            ApplyColor();
        }
    }

    void ApplyColor()
    {
        if (baseBodyRenderer != null)
        {
            baseBodyRenderer.color = skinColors[currentIndex];
        }
    }
}