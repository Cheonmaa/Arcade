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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Resume")
        {
            texts = new List<TextMeshProUGUI>();
            List<RoundList> tempRoundList = new List<RoundList>();

            foreach (RoundId roundId in Enum.GetValues(typeof(RoundId)))
            {
                string tag1 = $"Stats{roundId}";
                string tag2 = $"Stats{roundId}P2";
                string scoreTag = roundId.ToString(); 
                List<TextMeshProUGUI> tmpList = new List<TextMeshProUGUI>();
                AddTextFromTag(tmpList, tag1);
                AddTextFromTag(tmpList, tag2);
                AddTextFromTag(tmpList, scoreTag);
                if (tmpList.Count > 0)
                {
                    RoundList rl = new RoundList
                    {
                        roundId = roundId,
                        texts = tmpList
                    };
                    tempRoundList.Add(rl);
                }
            }
            roundList = tempRoundList.ToArray();
        }
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

    public void SetRoundText(RoundId roundId, int textIndex, string text)
    {
        foreach (var round in roundList)
        {
            if (round.roundId == roundId)
            {
                if (textIndex >= 0 && textIndex < round.texts.Count)
                {
                    round.texts[textIndex].text = text;
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
    
    public void CollectTextMeshProsWithTags(params string[] tags)
    {
        texts = new List<TextMeshProUGUI>();

        foreach (var tag in tags)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in taggedObjects)
            {
                TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
                if (tmp != null && !texts.Contains(tmp))
                {
                    texts.Add(tmp);
                }
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