using UnityEngine;
using TMPro;

public class SceneData : MonoBehaviour
{
    public TextMeshProUGUI roundText;

    void Start()
    {
        if (GameStats.instance == null)
        {
            roundText.text = "Round 1";
            return;
        }
        roundText.text = $"Round {GameStats.instance.roundsPlayed}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
