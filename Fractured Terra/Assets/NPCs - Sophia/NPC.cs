using System.Collections;
using UnityEngine;
using TMPro;

public class TutNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController playerController; // Helps pause movement
    
    public NpcDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract() // NPC can be interacted with when no dialogue is active
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null) return;
        if (isDialogueActive) // TODO: Or pause is active and dialogue is not
        {
            NextLine(); // Goes to next dialogue line dialogue is active
        }
        else
        {
            StartDialogue(); // Starts talking to NPC
        }
    }
    void StartDialogue() // Controls UI display
    {
            isDialogueActive = true;
            dialogueIndex = 0;
            playerController.CanMove = false; // Pause player movement
            
            nameText.SetText(dialogueData.npcName);
            dialoguePanel.SetActive(true);

            StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            // Skip typing animation and show full line
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex >= dialogueData.dialogueLines.Length)
        {
            // If another line, type next line
            StartCoroutine(TypeLine());
        }
        else
        {
            // Stop dialogue
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter; // Go through every letter in dialogue line
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false; // Line complete
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText(""); // Reset text
        dialoguePanel.SetActive(false); // Close dialogue panel
        playerController.CanMove = true; // Unpause the game
    }
}
