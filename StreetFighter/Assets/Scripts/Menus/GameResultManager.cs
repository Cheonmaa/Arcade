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
    [SerializeField] private RoundList[] roundList;
    private List<TextMeshProUGUI> texts;

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

    void Start()
    {
        SetRoundTexts(VictorySystem.instance.roundsPlayed);
    }

    public void OnClickRestartButton()
    {
        VictorySystem.instance.roundOver = false;
        VictorySystem.instance.victory = false;
        ResetTexts();
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

    public void SetRoundText(RoundId roundId, string text)
    {
        foreach (var round in roundList)
        {
            if (round.roundId == roundId)
            {
                foreach (var t in round.texts)
                {
                    t.text = text;
                }
            }
        }
    }
    
    public void ResetTexts()
    {
        foreach (var round in roundList)
        {
            foreach (var t in round.texts)
            {
                t.text = string.Empty;
            }
        }
    }
}

[System.Serializable]
public struct RoundList
{
    [SerializeField] public RoundId roundId;
    [SerializeField] public List<TextMeshProUGUI> texts;
}