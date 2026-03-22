using UnityEngine;

public class HairControl : MonoBehaviour
{
    public SpriteRenderer shortHair;
    public SpriteRenderer longHair;

    private bool isShort = true;

    private Color[] hairColors;
    private int currentColor = 0;

    void Start()
    {
        // define colors
        hairColors = new Color[5];

        ColorUtility.TryParseHtmlString("#C78235", out hairColors[0]); // ginger
        ColorUtility.TryParseHtmlString("#dcbd92", out hairColors[1]); // blond
        ColorUtility.TryParseHtmlString("#805A40", out hairColors[2]); // brown
        ColorUtility.TryParseHtmlString("#000000", out hairColors[3]); // purple
        ColorUtility.TryParseHtmlString("#FF8DA1", out hairColors[4]); // hot pink

        ApplyColor();
    }

    void Update()
    {
        // SWITCH HAIR TYPE
        if (Input.GetKeyDown(KeyCode.H))
        {
            isShort = !isShort;

            shortHair.enabled = isShort;
            longHair.enabled = !isShort;
        }

        // CHANGE COLOR
        if (Input.GetKeyDown(KeyCode.J))
        {
            currentColor = (currentColor + 1) % hairColors.Length;
            ApplyColor();
        }
    }

    void ApplyColor()
    {
        shortHair.color = hairColors[currentColor];
        longHair.color = hairColors[currentColor];
    }
}
