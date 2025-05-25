using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource audio;

    void Start() {StartCoroutine(PlayAudioWithDelay());}

    IEnumerator PlayAudioWithDelay()
    {
        yield return new WaitForSeconds(2f);
        audio.Play();
    }
}
