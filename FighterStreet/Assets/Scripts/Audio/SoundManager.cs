using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    PUNCH,
    JAB,
    KICK,
    FAIL,
    BLOCK,
    HURT,
    WIN,
    FOOTSTEP,
    JUMP
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [Header("Audio Sources")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] soundList;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType soundType, float volume = 1f)
    { 
        instance.audioSource.PlayOneShot(instance.soundList[(int)soundType], volume);    
    }
}