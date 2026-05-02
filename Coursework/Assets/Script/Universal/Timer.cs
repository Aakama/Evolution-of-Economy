using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public float TimeRemaining;

    public PerishibilityManager PerishabilityManager;

    public float Duration;
    public bool IsRunning = false;

    // This is the "Broadcast" we talked about
    public UnityEvent OnTimerExpired;

    void Awake()
    {
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

    public void StartTimer(float newDuration)
    {
        if (newDuration <= 0)
            return;

        TimeRemaining = newDuration;
        this.Duration = newDuration;
        IsRunning = true;
        Debug.Log("Timer started for: " + Duration + " seconds");
    }

    void Update()
    {
        if (IsRunning)
        {
            TimeRemaining -= Time.deltaTime;

            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                IsRunning = false;

                PerishibilityManager manager = Object.FindFirstObjectByType<PerishibilityManager>();
                if (manager != null)
                {
                    manager.TriggeredPerishability();
                }
                else
                {
                    Debug.LogError("Timer can't find the PerishabilityManager in this scene!");
                }

                // Fire the signal!
                // OnTimerExpired?.Invoke();
                Debug.Log("Timer reached zero. Signal sent!");
            }
        }
    }

    public void StopTimer()
    {
        IsRunning = false;
        TimeRemaining = 0;
        Debug.Log("Timer stopped manually.");
    }
}
