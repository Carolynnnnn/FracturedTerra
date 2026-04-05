using UnityEngine;

public class BossSceneManager : MonoBehaviour
{
    [Header("References")]
    public GameObject gemChest;
    public GameObject exitPortal;

    [Header("Positions")]
    public Vector3 chestRevealPosition = new Vector3(1.3f, 5.11f, 0f);
    public Vector3 portalRevealPosition = new Vector3(3.67f, 5.36f, 0f);

    private bool chestOpened = false;
    private bool portalRevealed = false;
    private int previousGemCount = 0;

    void Update()
    {
        if (!chestOpened && gemChest != null && 
            GemManager.gemCount > previousGemCount)
        {
            chestOpened = true;
            gemChest.transform.position = chestRevealPosition;
        }
        
        if (chestOpened && !portalRevealed && 
            GemManager.gemCount > previousGemCount + 1)
        {
            portalRevealed = true;
            exitPortal.transform.position = portalRevealPosition;
            previousGemCount = GemManager.gemCount;
        }
    }

    public void OnBossDefeated()
    {
        previousGemCount = GemManager.gemCount;
    }
}
