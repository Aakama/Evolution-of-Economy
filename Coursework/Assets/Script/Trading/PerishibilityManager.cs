using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerishibilityManager : MonoBehaviour
{
    public static PerishibilityManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // This is the magic line that keeps it alive
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // If a new one is created in a loaded scene, kill it immediately
            Destroy(gameObject);
            return;
        }
    }

    public void TriggeredPerishability()
    {
        Debug.Log("Item rotted");

        NpcTrading.currentDay = 1;

        // SceneManager.LoadScene("GameOverScene");
    }
}
