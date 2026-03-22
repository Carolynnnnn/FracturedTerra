using System.Collections;
using UnityEngine;
using TMPro;

public class TutNPC : MonoBehaviour, IInteractable
{
    public NpcDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null) return;
        if (isDialogueActive) // TODO: Or pause is active and dialogue is not
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }
    void StartDialogue()
    {
            isDialogueActive = true;
            dialogueIndex = 0;
            
            nameText.SetText(dialogueData.npcName);
            dialoguePanel.SetActive(true);
            // TODO: Set pause to true

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
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        // TODO: Unpause the game
    }
}
