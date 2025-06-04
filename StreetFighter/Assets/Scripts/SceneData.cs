using UnityEngine;
using TMPro;

public class SceneData : MonoBehaviour
{
    public TextMeshProUGUI roundText;

    void Start()
    {
        roundText.text = $"Round {GameManager.RoundNumber}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
