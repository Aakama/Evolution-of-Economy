using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void OnClickRespawnButton()
    {
        NpcTrading.currentDay = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickContinueButton()
    {
        NpcTrading.currentDay = 1;
        SceneManager.LoadScene("GoldEra");
    }
}
