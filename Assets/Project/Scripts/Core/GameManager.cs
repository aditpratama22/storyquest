using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("State Sesi")]
    [SerializeField] private int totalPoints;
    [SerializeField] private string selectedStoryId = string.Empty;
    [SerializeField] private string playerName = "Pemain";

    public int TotalPoints => totalPoints;
    public string SelectedStoryId => selectedStoryId;
    public string PlayerName => playerName;

    private SessionData _currentSession;
    public SessionData CurrentSession => _currentSession;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        totalPoints = PlayerPrefs.GetInt("TotalPoints", 0);
    }

    public void BeginSession(string storyId, string storyTitle)
    {
        _currentSession = new SessionData(storyId, storyTitle);
        selectedStoryId = storyId;
        Debug.Log($"[GameManager] Sesi dimulai: {storyTitle}");
    }

    public void RecordObjective(int points)
    {
        if (_currentSession == null) return;
        _currentSession.pointsEarned += points;
        _currentSession.objectivesCompleted++;
        AddPoints(points);
    }

    public void AddPoints(int amount)
    {
        if (amount <= 0) return;
        totalPoints += amount;
        PlayerPrefs.SetInt("TotalPoints", totalPoints);
        PlayerPrefs.Save();
        Debug.Log($"[GameManager] +{amount} poin. Total: {totalPoints}");
    }

    public void EndSession()
    {
        if (_currentSession != null)
        {
            _currentSession = null;
            Debug.Log("[GameManager] Sesi berakhir.");
        }
    }

    public void SelectStory(string storyId)
    {
        selectedStoryId = storyId;
    }

    public void ResetSession()
    {
        totalPoints = 0;
        selectedStoryId = string.Empty;
        PlayerPrefs.SetInt("TotalPoints", 0);
        PlayerPrefs.Save();
    }
}