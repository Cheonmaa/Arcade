using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;

    [Header("Leaderboard Setup")]
    public GameObject leaderboardLinePrefab;
    public Transform leaderboardContainer;

    private Dictionary<string, int> leaderboard = new Dictionary<string, int>();
    private List<GameObject> activeLines = new List<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadLeaderboard();
    }

    private void Start()
    {
        DisplayLeaderboard();
    }

    private void LoadLeaderboard()
    {
        leaderboard.Clear();
        int count = PlayerPrefs.GetInt("LeaderboardCount", 0);

        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString($"LeaderboardPlayer_{i}", "");
            int score = PlayerPrefs.GetInt($"LeaderboardScore_{i}", 0);
            if (!string.IsNullOrEmpty(name))
                leaderboard[name] = score;
        }
    }

    private void SaveLeaderboard()
    {
        var sorted = leaderboard.OrderByDescending(entry => entry.Value).ToList();

        PlayerPrefs.SetInt("LeaderboardCount", sorted.Count);
        for (int i = 0; i < sorted.Count; i++)
        {
            PlayerPrefs.SetString($"LeaderboardPlayer_{i}", sorted[i].Key);
            PlayerPrefs.SetInt($"LeaderboardScore_{i}", sorted[i].Value);
        }
        PlayerPrefs.Save();
    }

    public void AddScore(string playerName, int scoreToAdd)
    {
        if (leaderboard.ContainsKey(playerName))
            leaderboard[playerName] += scoreToAdd;
        else
            leaderboard[playerName] = scoreToAdd;

        SaveLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        foreach (var obj in activeLines)
            Destroy(obj);
        activeLines.Clear();
        var sorted = leaderboard.OrderByDescending(entry => entry.Value).ToList();
        foreach (var entry in sorted)
        {
            GameObject line = Instantiate(leaderboardLinePrefab, leaderboardContainer);
            LeaderboardLineUI lineUI = line.GetComponent<LeaderboardLineUI>();
            lineUI.SetLine(entry.Key, entry.Value);
            activeLines.Add(line);
        }
    }
}
