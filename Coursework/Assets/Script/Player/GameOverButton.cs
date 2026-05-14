using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void OnClickRespawnButton()
    {
        NpcTrading.currentDay = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickContinueButton()
    {
        NpcTrading.currentDay = 2;
        SceneManager.LoadScene("GoldEra");
    }
}
