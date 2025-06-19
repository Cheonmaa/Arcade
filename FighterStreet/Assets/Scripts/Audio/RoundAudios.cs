using UnityEngine;
using System.Collections;

public class RoundAudios : MonoBehaviour
{
    [Header("Audio Sources")]
    public new AudioSource audio;
    public AudioSource audio2;
    public AudioSource audio3;

    void Start() { StartCoroutine(PlayAudioWithDelay()); StartCoroutine(NextSceneDelay()); }

    IEnumerator PlayAudioWithDelay()
    {
        if (GameStats.instance == null){

            yield return new WaitForSeconds(2f);
            audio.Play();
        }
        else if (GameStats.instance.roundsPlayed == 1)
        {
            yield return new WaitForSeconds(2f);
            audio2.Play();
        }
        else if (GameStats.instance.roundsPlayed == 2)
        {
            yield return new WaitForSeconds(2f);
            audio3.Play();
        }
    }
    
    IEnumerator NextSceneDelay()
    {
        yield return new WaitForSeconds(4f);
        GameManager.instance.ChangeScene("VERSUS");
    }
}
