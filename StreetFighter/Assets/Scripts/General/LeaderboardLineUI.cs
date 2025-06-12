using TMPro;
using UnityEngine;

public class LeaderboardLineUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    public void SetLine(string playerName, int score)
    {
        nameText.text = playerName;
        scoreText.text = score.ToString();
    }
}
