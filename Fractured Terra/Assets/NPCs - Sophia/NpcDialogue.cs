using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NpcDialogue : ScriptableObject // Set up for reusable dialogue obj
{
    public string npcName; // Holds NPC's name
    public string[] dialogueLines; // Holds lines of dialogue
    public float typingSpeed = 0.05f; // Holds speech speed
    public int[] dialogueStates; // First number is last index of lines spoken in default
                                 // Second number is last index of lines spoken when quest completed
                                 // Ignored for non-quest NPCs
    // TODO: Add an item wanted field once get and set are implemented in inventory
}
