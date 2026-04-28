using UnityEngine;

public class NpcTrading : MonoBehaviour
{
    //Makes it easier to find for other scripts
    public static NpcTrading Instance;

    public int currentDay = 1;

    void Awake()
    {
        Instance = this;
    }

    public void ExecuteTradeLogic(NpcOccupations npcA, NpcOccupations npcB)
    {
        CheckPreference(npcA, npcB);
    }

    public void CheckPreference(NpcOccupations npcA, NpcOccupations npcB)
    {
        if (npcA.Preference == npcB.Product.ItemName || npcA.Product.ItemName == npcB.Preference)
        {
            Debug.Log("The trade is compatable");
        }
    }

    public void CheckPerishability(NpcOccupations npcA, NpcOccupations npcB)
    {
        if (npcA.Product.IsPerishable || npcB.Product.IsPerishable)
        {
            //call perishability manager
        }
    }

    public void CheckItemValue(NpcOccupations npcA, NpcOccupations npcB)
    {
        // if (npc)
    }
}
