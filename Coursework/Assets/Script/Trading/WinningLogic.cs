using UnityEngine;

public class WinningLogic : MonoBehaviour
{
    public NpcOccupations Player;
    public static NpcTrading Instance;

    public void CheckWinningCondition()
    {
        Player = GetComponent<NpcOccupations>();

        if (Player.Product.ItemName == "Apple" && Player.MyJobs == Occupations.AppleFarmer)
        {
            // Trigger winning scenario
            Debug.Log("Congratulations! You've won the game!");

            NpcTrading.Instance.EndOfDay();
            // Reset the scene or load a new one for the winning scenario
            // You can add more logic here, such as loading a new scene or displaying a UI message
        }
        //  else if (/* Your winning condition here */)
        // }
        // {
        //     // Check if the player has traded for a certain number of items or reached a certain value
        //     // If the condition is met, trigger the winning scenario (e.g., display a message, load a new scene, etc.)
        // }
        // This could be based on the number of trades, the value of items, or any other criteria you choose
    }
}
