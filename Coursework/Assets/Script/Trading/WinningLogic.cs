using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningLogic : MonoBehaviour
{
    public NpcOccupations Player;

    // public static NpcTrading Instance;

    public void CheckWinningCondition()
    {
        Player = GetComponent<NpcOccupations>();

        if (Player.Product.ItemName == "Apple" && Player.MyJobs == Occupations.AppleFarmer)
        {
            if (NpcTrading.currentDay != 2)
            {
                // Trigger winning scenario
                Debug.Log("Congratulations! You've won the game!");

                NpcTrading.Instance.EndOfDay();
                // Reset the scene or load a new one for the winning scenario
                // You can add more logic here, such as loading a new scene or displaying a UI message
            }
            else
            {
                NpcTrading.currentDay += 1;
            }
        }
    }
}
