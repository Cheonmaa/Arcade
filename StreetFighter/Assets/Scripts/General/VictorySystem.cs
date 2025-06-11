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
        if (deadPlayer == null) return;
        string winnerTag = (deadPlayer.tag == "Player1") ? "Player2" : "Player1";
        currentRoundText.text = $"{winnerTag} won!";
    }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += (_, __) => UpdateUI();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= (_, __) => UpdateUI();
    }

    private void UpdateUI()
    {
        InstantiateVariables();

        if (currentRoundText != null)
            currentRoundText.text = $"Round {GameStats.instance.roundsPlayed + 1}";

        if (playerName != null && player1 != null)
            playerName.text = player1.tag;

        if (player2Name != null && player2 != null)
            player2Name.text = player2.tag;
    }

    public void Start()
    {
        InstantiateVariables();
        victory = false;
        roundOver = false;
        playerName.text = player1.tag;
        player2Name.text = player2.tag;
        currentRoundText.text = $"Round {GameStats.instance.roundsPlayed + 1} ";
    }
    void Update()
    {
        WinCondition();
        RoundOver();
        GetPlayer1Stats();
        GetPlayer2Stats();
    }

    public void RoundOver()
    {
        if (player1 == null || player2 == null) return;

        if (player1.currentHealth <= 0 || player2.currentHealth <= 0 && victory == false)
        {
            PlayerGetWins();
            roundOver = true;
            GameStats.instance.roundsPlayed++;
            if (GameStats.instance.player1Score >= 2 || GameStats.instance.player2Score >= 2)
            {
                OnVictory();
            }
            else
            {
                LoadNextScene();
                GameManager.instance.ChangeScene("SELECTCHAR");
            }

        }
    }


    public void WinCondition()
    {
        if (player1 == null || player2 == null) return;
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
        yield return new WaitForSeconds(5f);
        GameManager.instance.ChangeScene("Resume");
    }

    private void InstantiateVariables()
    {
        if (player1 == null)
        {
            player1 = GameObject.FindWithTag("Player1")?.GetComponent<Player>();
        }
        if (player2 == null)
        {
            player2 = GameObject.FindWithTag("Player2")?.GetComponent<Player>();
        }
        if (currentRoundText == null)
        {
            currentRoundText = GameObject.FindWithTag("CurrentRounds")?.GetComponent<TextMeshProUGUI>();
        }
        if (playerName == null)
        {
            playerName = GameObject.FindWithTag("playerName")?.GetComponent<TextMeshProUGUI>();
        }
        if (player2Name == null)
        {
            player2Name = GameObject.FindWithTag("playerName2")?.GetComponent<TextMeshProUGUI>();
        }
    }

    public void PlayerGetWins()
    {
        if (player1.currentHealth <= 0)
        {
            GameStats.instance.player2Score++;
        }
        else if (player2.currentHealth <= 0)
        {
            GameStats.instance.player1Score++;
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);
    }

    public void OnVictory()
    {
        if (GameStats.instance.player1Score >= 2 || GameStats.instance.player2Score >= 2)
        {
            if (victory) return;
            victory = true;
            StartCoroutine(VictoryScreen());
            return;
        }
    }

    public int GetPlayer1Stats()
    {
        if (roundOver)
        {
            player1CurrentHealth = GameStats.instance.player1RemainingHealth;
            return player1CurrentHealth;
        }
        GameStats.instance.ResetPlayerStats();
    }

    public int GetPlayer2Stats()
    {
        if (roundOver)
        {
            player2CurrentHealth = GameStats.instance.player2RemainingHealth;
            return player2CurrentHealth;
        }
        GameStats.instance.ResetPlayerStats();
    }
}