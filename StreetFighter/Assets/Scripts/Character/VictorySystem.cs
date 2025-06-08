/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class VictorySystem : MonoBehaviour
{
    [Header("Components")]
    public Player player1;
    public Player player2;

    [Header("UI")]
    public TextMeshProUGUI playerName;

    public void AnnounceWinner(Player deadPlayer)
    {
        string winnerTag = (deadPlayer.tag == "Player1") ? "Player2" : "Player1";
        playerName.text = $"{winnerTag} won!";
    }

    void Update()
    {
        if (player1.health <= 0)
        {
            AnnounceWinner(player1);
        }
        else if (player2.health <= 0)
        {
            AnnounceWinner(player2);
        }
    }
}*/
