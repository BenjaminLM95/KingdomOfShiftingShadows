using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mainMenuClip; // The music clip for the main menu
    [SerializeField] private AudioClip gamePlayClip; // The music clip for the level
    [SerializeField] private AudioClip resultScreen; // The music when the player lose and has the result screen
    [SerializeField] private bool isPlaying = false; // Flag to check if music is playing
    public AudioMixer audioMixer; // Reference to the AudioMixer for volume control


    //Plays the current music clip and sets the isPlaying flag to true.
    public void PlayMusic(bool loop, string clipName)
    {
        switch (clipName)
        {
            case "mainmenu":
                audioSource.clip = mainMenuClip;
                break;
            case "Gameplay":
                audioSource.clip = gamePlayClip;
                break;
            case "Result":
                audioSource.clip = resultScreen;
                break;
            default:
                Debug.LogWarning("Invalid music clip name: " + clipName);
                return;
        }
        audioSource.loop = loop;
        audioSource.Play();
        isPlaying = true;
    }

    public void SetVolume(float volume, string groupName)
    {
        audioMixer.SetFloat(groupName, volume);
    }

}
