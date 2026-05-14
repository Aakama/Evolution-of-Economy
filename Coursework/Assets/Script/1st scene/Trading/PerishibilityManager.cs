using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerishibilityManager : MonoBehaviour
{
    public GameObject GameOverPanel;
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. Find the main UI Canvas
        GameObject canvas = GameObject.Find("Death");

        if (canvas != null)
        {
            // 2. Look for the panel inside the canvas (even if inactive)
            Transform panelTransform = canvas.transform.Find("Panel");

            if (panelTransform != null)
            {
                GameOverPanel = panelTransform.gameObject;
            }
        }
    }

    public void GameOverResetScene()
    {
        Debug.Log("Item rotted");

        NpcTrading.currentDay = 2;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TriggeredPerishability()
    {
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
            Debug.Log("GameOverPanel activated due to timer expiration");
        }
        else
        {
            Debug.LogError("GameOverPanel is not assigned in the PerishabilityManager!");
            GameOverResetScene(); // Fallback to resetting the scene if the panel is missing
        }
    }
}
