using UnityEngine;
using UnityEngine.UI;

public class OutfitControl : MonoBehaviour
{
    [Header("Preview")]
    public Image previewOutfit;

    [Header("Real Player")]
    public SpriteRenderer playerOutfit;

    [Header("Animator")]
    public PlayerVisualAnimator animatorScript;

    [Header("Outfits (3 only)")]
    public Sprite[] outfits; // preview / default still image only

    private static int currentIndex = 0;
    // 0 = no outfit
    // 1-3 = outfits

    void Start()
    {
        ApplyOutfit();
    }

    public void NextOutfit()
    {
        currentIndex++;

        if (currentIndex > outfits.Length)
            currentIndex = 0;

        ApplyOutfit();
    }

    public void PreviousOutfit()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = outfits.Length;

        ApplyOutfit();
    }

    void ApplyOutfit()
    {
        if (animatorScript != null)
        {
            animatorScript.currentOutfitIndex = currentIndex;
            Debug.Log("OutfitControl set currentOutfitIndex to: " + currentIndex);
        }
        else
        {
            Debug.Log("animatorScript is NULL");
        }

        if (currentIndex == 0)
        {
            if (previewOutfit != null)
                previewOutfit.enabled = false;

            if (playerOutfit != null)
                playerOutfit.enabled = false;

            return;
        }

        Sprite selected = outfits[currentIndex - 1];

        if (previewOutfit != null)
        {
            previewOutfit.enabled = true;
            previewOutfit.sprite = selected;
            previewOutfit.preserveAspect = true;
        }

        if (playerOutfit != null)
        {
            playerOutfit.enabled = true;
            playerOutfit.sprite = selected;
        }
    }
}