using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NpcDialogue : ScriptableObject
{
    public string npcName;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
}
