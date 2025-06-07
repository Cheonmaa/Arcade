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

    private static GameResultManager instance;
    [Header("Game Result Manager")]
    [SerializeField] private RoundList[] roundList;
    private List<TextMeshProUGUI> texts;

    // Update is called once per frame
    public void Awake(){ instance = this; }

    void Update()
    {

    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("MENU");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}

[System.Serializable]
public struct RoundList
{
    [SerializeField] public RoundId roundId;
    [SerializeField] public List<TextMeshProUGUI> texts;
}