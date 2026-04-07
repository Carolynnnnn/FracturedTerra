using UnityEngine;

public class BossBorderTrigger : MonoBehaviour
{
    [Header("References")]
    public BoxCollider2D borderCollider;
    public FinalBoss finalBoss;

    [Header("Settings")]
    public float triggerX;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        ResetBorder();
    }

    void Update()
    {
        if (player == null) return;

        if (player.position.x > triggerX)
        {
            if (borderCollider != null)
                borderCollider.enabled = true;
            if (finalBoss != null && !finalBoss.attackEnabled)
                finalBoss.EnableAttack();
        }
        else
        {
            ResetBorder();
        }
    }

    public void ResetBorder()
    {
        if (borderCollider != null)
            borderCollider.enabled = false;
        if (finalBoss != null)
            finalBoss.DisableAttack();
        Debug.Log("Border reset!");
    }
}