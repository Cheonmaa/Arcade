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
        player1Score = 0;
        player2Score = 0;
        roundsPlayed = 0;
    }
}