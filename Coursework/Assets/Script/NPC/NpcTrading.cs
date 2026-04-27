using UnityEngine;

public class NpcTrading : MonoBehaviour
{
    //Makes it easier to find for other scripts
    public static NpcTrading Instance;

    public int currentDay = 1;

    void Awake() {
        Instance = this;
    }

    public void ExecuteTradeLogic(NpcOccupations npcA, NpcOccupations npcB){
        Debug.Log($"Logic Check: {npcA.MyJobs} meeting {npcB.MyJobs} on Day {currentDay}");
    }
}
