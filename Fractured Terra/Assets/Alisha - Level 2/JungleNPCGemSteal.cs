using UnityEngine;

// Attach to the JungleNPC GameObject alongside NPC.cs.
// Resets GemManager.gemCount to 0 the first time the player opens dialogue with this NPC.
[RequireComponent(typeof(NPC))]
public class JungleNPCGemSteal : MonoBehaviour
{
    private NPC _npc;
    private bool _stealDone;
    private bool _wasInteractable;

    private void Start()
    {
        _npc = GetComponent<NPC>();
        _wasInteractable = _npc.CanInteract();
    }

    private void Update()
    {
        if (_stealDone) return;

        bool canInteract = _npc.CanInteract();

        // Detects the moment dialogue becomes active (CanInteract flips false)
        if (_wasInteractable && !canInteract)
        {
            GemManager.gemCount = 0;
            _stealDone = true;
            Debug.Log("[JungleNPCGemSteal] Gems stolen, gemCount reset to 0");
        }

        _wasInteractable = canInteract;
    }
}
