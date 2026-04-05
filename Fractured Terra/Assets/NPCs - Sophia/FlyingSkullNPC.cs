using System.Collections;
using UnityEngine;
using TMPro;

// Standalone NPC script for the Flying Skull NPCs in Japneet_Level3.
// Uses distance-based detection instead of InteractionDetector.
// Does not modify the shared NPC.cs script.
public class FlyingSkullNPC : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerController;
    public NpcDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;

    [Header("Interaction")]
    public float interactRadius = 2f;

    [Header("Checkpoint")]
    [Tooltip("If checked, talking to this NPC sets a respawn checkpoint at its position.")]
    public bool isCheckpoint = false;

    [Header("Timer")]
    [Tooltip("If checked, talking to this NPC starts the time limit challenge.")]
    public bool startsTimer = false;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private Transform player;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
        if (ph != null) player = ph.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= interactRadius && Input.GetKeyDown(KeyCode.P))
            Interact();
    }

    void Interact()
    {
        if (dialogueData == null) return;

        if (isDialogueActive)
        {
            NextLine();
            return;
        }

        if (isCheckpoint)
        {
            PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
            if (ph != null) ph.SetCheckpoint(transform.position);
        }

        if (startsTimer && TimeLimitManager.Instance != null)
            TimeLimitManager.Instance.StartTimer();

        StartDialogue();
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        if (playerController != null) playerController.CanMove = false;

        nameText.SetText(dialogueData.npcName);
        dialoguePanel.SetActive(true);
        dialogueIndex = 0;

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex <= dialogueData.dialogueLines.Length - 1)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        if (playerController != null) playerController.CanMove = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
