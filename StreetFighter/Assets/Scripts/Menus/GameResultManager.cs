using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public enum RoundId
{
    Round1,
    Round2,
    Round3,
}



public class GameResultManager : MonoBehaviour
{

    public static GameResultManager instance { get; private set; }
    [Header("Game Result Manager")]
    private TextMeshProUGUI[] p1StatsTexts = new TextMeshProUGUI[3];
    private TextMeshProUGUI[] p2StatsTexts = new TextMeshProUGUI[3];
    private TextMeshProUGUI[] roundScoreTexts = new TextMeshProUGUI[3];
    public Button restartButton;
    public Button mainMenuButton;
    public Button quitButton;

    // Update is called once per frame
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += (_, __) => Start();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= (_, __) => Start();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "RESUME")
        {
            

        mainMenuButton = GameObject.FindGameObjectWithTag("Main").GetComponent<Button>();
        quitButton = GameObject.FindGameObjectWithTag("Quit").GetComponent<Button>();
        restartButton = GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>();

        restartButton.onClick.AddListener(OnClickRestartButton);
        mainMenuButton.onClick.AddListener(OnClickMainMenuButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
        }

        p1StatsTexts[0] = GameObject.FindGameObjectWithTag("StatsRound1")?.GetComponent<TextMeshProUGUI>();
        p1StatsTexts[1] = GameObject.FindGameObjectWithTag("StatsRound2")?.GetComponent<TextMeshProUGUI>();
        p1StatsTexts[2] = GameObject.FindGameObjectWithTag("StatsRound3")?.GetComponent<TextMeshProUGUI>();

        p2StatsTexts[0] = GameObject.FindGameObjectWithTag("StatsRound1P2")?.GetComponent<TextMeshProUGUI>();
        p2StatsTexts[1] = GameObject.FindGameObjectWithTag("StatsRound2P2")?.GetComponent<TextMeshProUGUI>();
        p2StatsTexts[2] = GameObject.FindGameObjectWithTag("StatsRound3P2")?.GetComponent<TextMeshProUGUI>();

        roundScoreTexts[0] = GameObject.FindGameObjectWithTag("Round1")?.GetComponent<TextMeshProUGUI>();
        roundScoreTexts[1] = GameObject.FindGameObjectWithTag("Round2")?.GetComponent<TextMeshProUGUI>();
        roundScoreTexts[2] = GameObject.FindGameObjectWithTag("Round3")?.GetComponent<TextMeshProUGUI>();

        UpdateUI();
    }
    private void UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            if (p1StatsTexts[i] != null)
            {
                p1StatsTexts[i].text = $"Health: {GameStats.instance.player1RemainingHealth[i]}\nDamage: {GameStats.instance.player1TotalDamageDealt[i]}";
            }
            if (p2StatsTexts[i] != null)
            {
                p2StatsTexts[i].text = $"Health: {GameStats.instance.player2RemainingHealth[i]}\nDamage: {GameStats.instance.player2TotalDamageDealt[i]}";
            }
            if (roundScoreTexts[i] != null)
            {
                int p1ScoreForRound = (GameStats.instance.player1RemainingHealth[i] > GameStats.instance.player2RemainingHealth[i]) ? 1 : 0;
                int p2ScoreForRound = (GameStats.instance.player2RemainingHealth[i] > GameStats.instance.player1RemainingHealth[i]) ? 1 : 0;
                roundScoreTexts[i].text = $"{p1ScoreForRound} - {p2ScoreForRound}";
            }
        }
    }

    public void OnClickRestartButton()
    {
        VictorySystem.instance.roundOver = false;
        VictorySystem.instance.victory = false;
        ResetTexts();
        ResetGameStats();
        SceneManager.LoadScene("SELECTCHAR");
    }

    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("MENU");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    public void ResetTexts()
    {
        for (int i = 0; i < 3; i++)
        {
            if (p1StatsTexts[i] != null)
            {
                p1StatsTexts[i].text = "Health: 0\nDamage: 0";
            }
            if (p2StatsTexts[i] != null)
            {
                p2StatsTexts[i].text = "Health: 0\nDamage: 0";
            }
            if (roundScoreTexts[i] != null)
            {
                roundScoreTexts[i].text = "0 - 0";
            }
        }
    }

    public void ResetGameStats()
    {
        GameStats.instance.player1Score = 0;
        GameStats.instance.player2Score = 0;
        GameStats.instance.player1RemainingHealth = new int[3] { 0, 0, 0 };
        GameStats.instance.player2RemainingHealth = new int[3] { 0, 0, 0 };
        GameStats.instance.player1TotalDamageDealt = new int[3] { 0, 0, 0 };
        GameStats.instance.player2TotalDamageDealt = new int[3] { 0, 0, 0 };
        GameStats.instance.roundsPlayed = 0;
    }
    
    
}