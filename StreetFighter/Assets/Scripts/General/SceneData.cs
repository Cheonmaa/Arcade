using UnityEngine;
using TMPro;

public class SceneData : MonoBehaviour
{
    public TextMeshProUGUI roundText;

    void Start()
    {
        int round = GameStats.instance != null ? GameStats.instance.roundsPlayed + 1 : 1;
        roundText.text = $"Round {round}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
