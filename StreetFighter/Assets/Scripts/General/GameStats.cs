using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public static GameStats instance { get; private set; }

    [Header("Game Stats")]
    public int player1Score = 0;
    public int player2Score = 0;
    public int roundsPlayed = 0;

    [Header("Players stats")]
    public int player1RemainingHealth;
    public int player2RemainingHealth;
    public int player1TotalDamageDealt;
    public int player2TotalDamageDealt;


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

    public void ResetPlayerStats()
    {
        player1RemainingHealth = 100; // Assuming full health is 100
        player2RemainingHealth = 100; // Assuming full health is 100
        player1TotalDamageDealt = 0;
        player2TotalDamageDealt = 0;
    }

    public void ResetStats()
    {
        player1Score = 0;
        player2Score = 0;
        roundsPlayed = 0;
    }
}