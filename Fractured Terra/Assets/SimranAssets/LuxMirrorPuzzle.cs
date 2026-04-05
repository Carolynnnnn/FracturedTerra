using UnityEngine;

public class LuxMirrorPuzzle : MonoBehaviour, IInteractable
{
    [Header("Mirror State")]
    private bool activated = false;

    [Header("Puzzle References (assign on all mirrors)")]
    public LuxMirrorPuzzle[] allMirrors;
    public GameObject exitDoor;

    [Header("Visuals")]
    public Color activatedColor = new Color(1f, 0.9f, 0.3f, 1f);
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CanInteract()
    {
        return !activated;
    }

    public void Interact()
    {
        if (activated) return;

        activated = true;

        if (spriteRenderer != null)
            spriteRenderer.color = activatedColor;

        CheckPuzzleComplete();
    }

    private void CheckPuzzleComplete()
    {
        if (allMirrors == null || exitDoor == null) return;

        foreach (var mirror in allMirrors)
        {
            if (mirror != null && !mirror.activated)
                return;
        }

        exitDoor.SetActive(false);
    }
}
