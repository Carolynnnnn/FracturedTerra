using UnityEngine;

// Attach to the JungleNPC GameObject alongside NPC.cs.
// Sets the starting gem count for Level 2, then resets it to 0
// the first time the player opens dialogue with this NPC.
[DefaultExecutionOrder(-1000)]
[RequireComponent(typeof(NPC))]
public class JungleNPCGemSteal : MonoBehaviour
{
    private NPC _npc;
    private bool _stealDone;
    private bool _wasInteractable;

    private void Awake()
    {
        GemManager.gemCount = 2;
        Debug.Log("[JungleNPCGemSteal] gemCount initialised to 2");
    }

    private void Start()
    {
        _npc = GetComponent<NPC>();
        _wasInteractable = _npc.CanInteract();
    }

    private void Update()
    {
        if (_stealDone) return;

        bool canInteract = _npc.CanInteract();

        if (_wasInteractable && !canInteract)
        {
            GemManager.gemCount = 0;
            _stealDone = true;
            Debug.Log("[JungleNPCGemSteal] Gems stolen, gemCount = " + GemManager.gemCount);
        }

        _wasInteractable = canInteract;
    }
}
