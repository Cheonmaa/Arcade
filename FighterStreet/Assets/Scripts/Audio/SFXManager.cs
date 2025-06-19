using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    [Header("Components")]
    public Player player;

    public void PlaySFX()
    {
        if (player.hitboxTouched)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
