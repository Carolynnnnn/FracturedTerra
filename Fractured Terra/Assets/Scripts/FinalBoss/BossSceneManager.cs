using UnityEngine;

public class BossSceneManager : MonoBehaviour
{
    [Header("References")]
    public GameObject gemChest;
    public GameObject exitPortal;

    [Header("Positions")]
    public Vector3 chestRevealPosition = new Vector3(1.3f, 5.11f, 0f);
    public Vector3 portalRevealPosition = new Vector3(3.67f, 5.36f, 0f);

    private bool bossDefeated = false;
    private bool portalRevealed = false;
    private int gemCountOnDefeat;

    void Update()
    {
        if (bossDefeated && gemChest != null)
        {
            gemChest.transform.position = chestRevealPosition;
        }
        
        if (bossDefeated && !portalRevealed && 
            GemManager.gemCount > gemCountOnDefeat)
        {
            portalRevealed = true;
            if (exitPortal != null)
                exitPortal.transform.position = portalRevealPosition;
        }
    }

    public void OnBossDefeated()
    {
        Debug.Log("OnBossDefeated called!");
        bossDefeated = true;
        gemCountOnDefeat = GemManager.gemCount;
    
        if (gemChest != null)
        {
            gemChest.transform.position = chestRevealPosition;
            Debug.Log("Chest moved to: " + chestRevealPosition);
        }
        else
            Debug.LogWarning("gemChest is null!");
    }
}