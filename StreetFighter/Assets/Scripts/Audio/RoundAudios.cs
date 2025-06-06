using UnityEngine;
using System.Collections;

public class RoundAudios : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource audio;

    void Start() { StartCoroutine(PlayAudioWithDelay()); StartCoroutine(NextSceneDelay()); }

    IEnumerator PlayAudioWithDelay()
    {
        yield return new WaitForSeconds(2f);
        audio.Play();
    }
    
    IEnumerator NextSceneDelay()
    {
        yield return new WaitForSeconds(4f);
        GameManager.instance.ChangeScene("VERSUS");
    }
}
