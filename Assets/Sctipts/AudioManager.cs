using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("OtherSounds")]
    public AudioClip openAudio;
    public AudioClip closeAudio;
    [Header("MiniGameSounds")]
    public AudioClip grabSound;
    public AudioClip setSound;
    public AudioClip swapSound;
    public AudioClip victorySound;

    AudioSource audioSource;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

            }
            return instance;
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string state)
    {
        switch (state)
        {
            case "open":
                audioSource.clip = openAudio;
                break;
            case "close":
                audioSource.clip = closeAudio;
                break;
            case "grab":
                audioSource.clip = grabSound;
                break;
            case "set":
                audioSource.clip = setSound;
                break;
            case "swap":
                audioSource.clip = swapSound;
                break;
            case "victory":
                audioSource.clip = victorySound;

                break;
        }
        audioSource.Play();
    }

}
