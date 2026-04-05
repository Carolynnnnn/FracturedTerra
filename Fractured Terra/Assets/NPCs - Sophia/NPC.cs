using System.Collections;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController playerController; // Helps pause movement

    public NpcDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public NpcState npcState; // Determines if NPC has a quest, and its state
    public InventoryManager inventoryManager; // Keeps track of player's inventory

    [Header("Checkpoint")]
    [Tooltip("If checked, talking to this NPC sets a respawn checkpoint at its position.")]
    public bool isCheckpoint = false;

    [Header("Timer")]
    [Tooltip("If checked, talking to this NPC starts the time limit challenge.")]
    public bool startsTimer = false;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private bool playerInRange; 

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.P))
            Interact();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
            playerInRange = false;
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null) return;
        if (isDialogueActive)
        {
            NextLine(); // Goes to next dialogue line dialogue is active
        }
        else
        { 
            // Check if NPC wants an item
            if (npcState == NpcState.Default && !string.IsNullOrEmpty(dialogueData.itemWantedName))
            {
                bool hadItem = inventoryManager.RemoveItemByName(dialogueData.itemWantedName);

                if (hadItem)
                {
                    npcState = NpcState.QuestComplete;
                    Debug.Log("Quest item given!");
                    
                    // Give item prize
                    if (dialogueData.itemName == "Gem") // If the prize is a gem
                    {
                        GemManager.gemCount++;
                    }
                    else // If the prize is a regular item
                    {
                        InventoryItem newItem = new InventoryItem( // Create new inventory item based on stock
                            dialogueData.itemName,
                            dialogueData.description,
                            dialogueData.icon,
                            dialogueData.maxLife,
                            dialogueData.canUse,
                            dialogueData.worldPrefab
                        );
                        inventoryManager.AddItem(newItem); // Adds item to inventory
                    }
                }
            }
            if (isCheckpoint)
        {
            PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
            if (ph != null) ph.SetCheckpoint(transform.position);
        }

        if (startsTimer && TimeLimitManager.Instance != null)
            TimeLimitManager.Instance.StartTimer();

        StartDialogue(); // Starts talking to NPC
        }
    }
    void StartDialogue() // Controls UI display
    {
            isDialogueActive = true;
            playerController.CanMove = false; // Pause player movement
            
            nameText.SetText(dialogueData.npcName);
            dialoguePanel.SetActive(true);
            
            switch (npcState) // Figure out which index to start on
            {
                case NpcState.QuestComplete:
                    dialogueIndex = dialogueData.dialogueStates[0] + 1; // Line after default
                    break;
                case NpcState.PostQuest:
                    dialogueIndex = dialogueData.dialogueStates[1] + 1; // Line after quest completion
                    break;
                default: // When in default or no quest state
                    dialogueIndex = 0;
                    break;
            }
            StartCoroutine(TypeLine()); // Start typing
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
        else if (++dialogueIndex <= getMaxIndex())
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
        if (npcState == NpcState.QuestComplete) npcState = NpcState.PostQuest; // Only show quest complete dialogue once
    }

    int getMaxIndex() // Finds last index of lines currently being read
    {
        switch(npcState)
        {
            case NpcState.Default:
                return dialogueData.dialogueStates[0]; // Gets index of last default line
            case NpcState.QuestComplete:
                return dialogueData.dialogueStates[1]; // Gets index of last completion line
            default: // When post quest or no quest
                return dialogueData.dialogueLines.Length - 1; // Gets last index of dialogue
        }
    }
}
