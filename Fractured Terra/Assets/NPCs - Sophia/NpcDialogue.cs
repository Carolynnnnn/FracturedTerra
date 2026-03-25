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
    public string itemWantedName; // Item's name
    
    [Header("Item Prize (Quest Givers only)")]
    // Build item prize
    // Note: if name is gem, adds one to total instead
    public string itemName; // Item's name
    public string description; // Item's description
    public Sprite icon; // Item's sprite
    public int maxLife = 1; // Max amount of uses an item has, 1 use by default
    public bool canUse; // Determines if the item can be used
    public GameObject worldPrefab; // Helps drop item if necessary
                                   // note: event items SHOULD NOT HAVE THIS, as they should never be dropped
}
