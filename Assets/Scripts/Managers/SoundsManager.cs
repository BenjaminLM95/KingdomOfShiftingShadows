using UnityEngine;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip slashSwordClip; // The music clip for the main menu
    //[SerializeField] private AudioClip gamePlayClip; // The music clip for the level
    [SerializeField] private bool isPlaying = false; // Flag to check if music is playing
    public AudioMixer audioMixer; // Reference to the AudioMixer for volume control
       

    public void PlaySound(string clipName) 
    {
        switch (clipName) 
        {
            case "SlashSword":
                audioSource.clip = slashSwordClip;
                break;
        }
        audioSource.Play();
        
    }

    public void SetVolume(float volume, string groupName)
    {
        audioMixer.SetFloat(groupName, volume);
    }
}
