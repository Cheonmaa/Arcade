using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class VictorySystem : MonoBehaviour
{
    public static VictorySystem instance { get; private set; }
    [Header("Components")]
    public Player player1;
    public Player player2;

    [Header("UI")]
    public TextMeshProUGUI currentRoundText;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI player2Name;

    [Header("Variables")]
    public bool victory = false;
    public bool roundOver = false;

    public void AnnounceWinner(Player deadPlayer)
    {
        string winnerTag = (deadPlayer.tag == "Player1") ? "Player2" : "Player1";
        currentRoundText.text = $"{winnerTag} won!";
    }


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

    public void Start()
    {
        victory = false;
        roundOver = false;
        playerName.text = player1.tag;
        player2Name.text = player2.tag;
        currentRoundText.text = $"Round {GameStats.instance.roundsPlayed + 1}";
    }
    void Update()
    {
        WinCondition();
        RoundOver();
    }

    public void RoundOver()
    {
        if (player1.currentHealth <= 0 || player2.currentHealth <= 0)
        {
            roundOver = true;
            GameStats.instance.roundsPlayed++;
            if (GameStats.instance.roundsPlayed >= 3)
            {
                victory = true;
                GameStats.instance.ResetStats();
                StartCoroutine(VictoryScreen());
            }
            SceneManager.LoadScene("SELECTCHAR");
        }
    }

    public void WinCondition()
    {
        if (player1.currentHealth <= 0)
        {
            AnnounceWinner(player1);
        }
        else if (player2.currentHealth <= 0)
        {
            AnnounceWinner(player2);
        }
    }

    IEnumerator VictoryScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Resume");
    }
}