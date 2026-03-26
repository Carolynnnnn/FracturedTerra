using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public int xp = 0;
    public int coins = 0;
    public int armor = 0;

    public int itemCollections = 0;
    public int npcInteractions = 0;
    public int navigationProgress = 0;

    public void RegisterActivity(ActivityType activity, int xpReward, int coinReward = 0)
    {
        xp += xpReward;
        coins += coinReward;

        switch (activity)
        {
            case ActivityType.ItemCollection:
                itemCollections++;
                break;

            case ActivityType.NPCInteraction:
                npcInteractions++;
                break;

            case ActivityType.EnvironmentalNavigation:
                navigationProgress++;
                break;
        }

        UpdateArmor();
    }

    void UpdateArmor()
    {
        if (xp >= 50)
        {
            armor = 2;
        }
        else if (xp >= 20)
        {
            armor = 1;
        }
        else
        {
            armor = 0;
        }
	    
	}
	void Update()
	{
    if (Input.GetKeyDown(KeyCode.K))
    {
        RegisterActivity(ActivityType.ItemCollection, 5, 1);
    }
	}

	
}