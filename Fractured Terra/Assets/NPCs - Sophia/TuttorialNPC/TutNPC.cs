using UnityEngine;
using TMPro;

public class TutNPC : MonoBehaviour
{
    public NpcDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
}
