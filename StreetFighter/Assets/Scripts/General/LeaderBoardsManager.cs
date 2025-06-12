using UnityEngine;
using System.Collections.Generic;

public class LeaderBoardsManager : MonoBehaviour
{
    public static LeaderBoardsManager Instance { get; private set; }

    private Dictionary<string, int> leaderboards = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(string leaderboardId, int score)
    {
        if (leaderboards.ContainsKey(leaderboardId))
        {
            leaderboards[leaderboardId] += score;
        }
        else
        {
            leaderboards[leaderboardId] = score;
        }
    }

    public int GetScore(string leaderboardId)
    {
        return leaderboards.ContainsKey(leaderboardId) ? leaderboards[leaderboardId] : 0;
    }
}