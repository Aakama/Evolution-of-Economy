using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcTrading : MonoBehaviour
{
    //Makes it easier to find for other scripts
    public static NpcTrading Instance;

    public Timer Timer;

    public WinningLogic WinningLogic;

    public static int currentDay = 1;

    void Awake()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This object stays alive between scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates when the scene reloads
        }
    }

    void Start() { }

    public void ExecuteTradeLogic(NpcOccupations ThisNpc, NpcOccupations OtherNpc)
    {
        if (currentDay == 1)
        {
            bool Won = CheckPreference(ThisNpc, OtherNpc);
            if (Won)
            {
                Debug.Log("Trade successful!");

                WinningLogic win = ThisNpc.GetComponent<WinningLogic>();
                if (win != null)
                {
                    win.CheckWinningCondition();
                }
            }
        }
        else if (currentDay == 2)
        {
            bool Won = CheckPreference(ThisNpc, OtherNpc);
            // RefreshTimer(ThisNpc);
            bool NotPerished = CheckPerishability(ThisNpc);

            if (NotPerished && Won)
            {
                WinningLogic win = ThisNpc.GetComponent<WinningLogic>();
                if (win != null)
                {
                    win.CheckWinningCondition();
                }
            }
        }
        // else if (currentDay >= 3)
        // {
        //     CheckItemValue(ThisNpc, OtherNpc);
        // }
    }

    public bool CheckPreference(NpcOccupations ThisNpc, NpcOccupations OtherNpc)
    {
        if (ThisNpc.Product.ItemName == OtherNpc.Preference)
        {
            Debug.Log("The trade is compatable");
            ThisNpc.MyJobs = OtherNpc.MyJobs;
            ThisNpc.NpcData();

            return true;
        }
        return false;
    }

    public bool CheckPerishability(NpcOccupations ThisNpc)
    {
        Timer timer = ThisNpc.GetComponent<Timer>();
        if (ThisNpc.Product.IsPerishable)
        {
            if (timer != null)
            {
                timer.StartTimer(ThisNpc.Product.Perishability);
            }
            else
            {
                timer.StopTimer();
            }

            if (timer.IsRunning)
            {
                return true;
            }
        }
        else
        {
            Debug.Log("The item is not perishable.");
            timer.StopTimer();
            return true;
        }
        return false;
    }

    public void CheckItemValue(NpcOccupations ThisNpc, NpcOccupations OtherNpc)
    {
        // if (npc)
    }

    public void EndOfDay()
    {
        currentDay++;
        Debug.Log("Day " + currentDay);

        Invoke("ResetScene", 2f); // Resets the scene after 2 seconds to show the new day
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RefreshTimer(NpcOccupations player)
    {
        Timer t = player.GetComponent<Timer>();
        if (t == null)
            return;

        // Logic: Is the NEW item perishable?
        if (player.Product.IsPerishable == true)
        {
            Debug.Log(player.Product.ItemName + " is perishable. Starting Timer.");
            t.StartTimer(player.Product.Perishability);
        }
        else
        {
            Debug.Log(player.Product.ItemName + " is permanent. Stopping Timer.");
            t.StopTimer();
        }
    }
}
