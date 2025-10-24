using UnityEngine;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip slashSwordClip; // The sound effect for the character swinging the sword
    [SerializeField] private AudioClip witchLaugh; // The sound effect for the witch when appears
    [SerializeField] private AudioClip witchDeadClip; // The sound effect for the witch when dies
    [SerializeField] private AudioClip zombieDefeatClip; // The sound effect for the zombie when is defeated 
    [SerializeField] private AudioClip zombieFadedClip; // the sound effect for the zombie when fades
    [SerializeField] private bool isPlaying = false; // Flag to check if music is playing
    public AudioMixer audioMixer; // Reference to the AudioMixer for volume control
       

    public void PlaySound(string clipName) 
    {
        switch (clipName) 
        {
            case "SlashSword":
                audioSource.clip = slashSwordClip;
                break;
            case "WitchLaugh":
                audioSource.clip = witchLaugh;
                break;
            case "WitchDead":
                audioSource.clip = witchDeadClip;
                break;
            case "ZombieDefeated":
                audioSource.clip = zombieDefeatClip;
                break;
            case "ZombieScreams":
                audioSource.clip = zombieFadedClip;
                break; 
        }
        audioSource.Play();
        
    }

    public void SetVolume(float volume, string groupName)
    {
        audioMixer.SetFloat(groupName, volume);
    }
}
