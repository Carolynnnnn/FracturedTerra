using UnityEngine;
using TMPro;

// Manages the 3-key time limit challenge.
// Start it by calling TimeLimitManager.Instance.StartTimer() from an NPC.
// Keys register themselves by calling TimeLimitManager.Instance.KeyCollected().
//
// Setup:
//   1. Create Empty in the scene → name it "TimeLimitManager"
//   2. Attach this script
//   3. Assign Timer Text to a TMP text object on your Canvas (shows the countdown)
public class TimeLimitManager : MonoBehaviour
{
    public static TimeLimitManager Instance;

    [Header("Settings")]
    public float timeLimit = 120f;
    public int keysRequired = 3;

    [Header("UI")]
    [Tooltip("TMP text that displays the countdown. Leave blank if you have no timer UI yet.")]
    public TMP_Text timerText;

    private float timeRemaining;
    private int keysCollected = 0;
    private bool isActive = false;
    private bool isComplete = false;

    public int KeysCollected => keysCollected;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isActive || isComplete) return;

        timeRemaining -= Time.deltaTime;

        if (timerText != null)
        {
            int mins = Mathf.FloorToInt(timeRemaining / 60f);
            int secs = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.SetText($"{mins}:{secs:00}");
        }

        if (timeRemaining <= 0f)
            TimeUp();
    }

    public void StartTimer()
    {
        if (isActive) return;
        timeRemaining = timeLimit;
        keysCollected = 0;
        isActive = true;
        isComplete = false;
        Debug.Log("[TimeLimitManager] Timer started.");
    }

    public void KeyCollected()
    {
        if (!isActive || isComplete) return;

        keysCollected++;
        Debug.Log($"[TimeLimitManager] Key collected: {keysCollected}/{keysRequired}");

        if (keysCollected >= keysRequired)
            Complete();
    }

    void Complete()
    {
        isComplete = true;
        isActive = false;
        if (timerText != null) timerText.SetText("Done!");
        Debug.Log("[TimeLimitManager] All keys collected in time!");
    }

    void TimeUp()
    {
        isActive = false;
        isComplete = true;
        if (timerText != null) timerText.SetText("0:00");
        Debug.Log("[TimeLimitManager] Time's up! Killing player.");

        PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();
        if (ph != null) ph.Kill();
    }
}
