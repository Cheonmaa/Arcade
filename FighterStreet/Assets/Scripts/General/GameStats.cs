using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public static GameStats instance { get; private set; }

    [Header("Game Stats")]
    public int player1Score = 0;
    public int player2Score = 0;
    public int roundsPlayed = 0;

    [Header("Players stats")]
    public int[] player1RemainingHealth = new int[3];
    public int[] player2RemainingHealth = new int[3];
    public int[] player1TotalDamageDealt = new int[3];
    public int[] player2TotalDamageDealt = new int[3];

    [System.Serializable]
    public struct RoundResult
    {
        public string player1Stats;
        public string score;
        public string player2Stats;
    }

    public List<RoundResult> roundResults = new List<RoundResult>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ResetStats()
    {
        roundsPlayed = 0;
        player1Score = 0;
        player2Score = 0;
        for (int i = 0; i < 3; i++)
        {
            player1RemainingHealth[i] = 100;
            player2RemainingHealth[i] = 100;
            player1TotalDamageDealt[i] = 0;
            player2TotalDamageDealt[i] = 0;
        }
    }
}
