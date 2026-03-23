using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NpcDialogue : ScriptableObject // Set up for reusable dialogue obj
{
    public string npcName; // Holds NPC's name
    public string[] dialogueLines; // Holds lines of dialogue
    public float typingSpeed = 0.05f; // Holds speech speed
}
