using UnityEngine;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour
{

    public static SoundsManager instance;
    [SerializeField] private AudioSource soundFXObject; 

    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip slashSwordClip; // The sound effect for the character swinging the sword
    [SerializeField] private AudioClip witchLaugh; // The sound effect for the witch when appears
    [SerializeField] private AudioClip witchDeadClip; // The sound effect for the witch when dies
    [SerializeField] private AudioClip zombieDefeatClip; // The sound effect for the zombie when is defeated 
    [SerializeField] private AudioClip zombieFadedClip; // the sound effect for the zombie when fades
    [SerializeField] private AudioClip getCoinClip; // The sound effect when you get money;
    [SerializeField] private AudioClip useCoinClip; // The sound when you buy something; 
    [SerializeField] private AudioClip skyHurtClip; // The sound when Sky (the player) get hit by the enemy
    [SerializeField] private bool isPlaying = false; // Flag to check if music is playing
    public AudioMixer audioMixer; // Reference to the AudioMixer for volume control

    public float sfxVolume = 1; 

    private void Awake()
    {
       if(instance == null) 
        {
            instance = this; 
        }
    }

    /*
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
        
    } */

    private void Start()
    {
        sfxVolume = 1; 
    }

    public void SetSound(string clipName, AudioSource _audioSource)
    {
        switch (clipName)
        {
            case "SlashSword":
                _audioSource.clip = slashSwordClip;
                break;
            case "WitchLaugh":
                _audioSource.clip = witchLaugh;
                break;
            case "WitchDead":
                _audioSource.clip = witchDeadClip;
                break;
            case "ZombieDefeated":
                _audioSource.clip = zombieDefeatClip;
                break;
            case "ZombieScreams":
                _audioSource.clip = zombieFadedClip;
                break;
            case "GetMoney":
                _audioSource.clip = getCoinClip;
                break;
            case "BuySound":
                _audioSource.clip = useCoinClip;
                break;
            case "PlayerHurt":
                _audioSource.clip = skyHurtClip;
                break; 
        }
        

    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    

    public void PlaySoundFXClip(string clipName, Transform spawnTransform) 
    {
        // Spawn the gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // assign the audioClip
        SetSound(clipName, audioSource);

        // Assign Volume
        audioSource.volume = sfxVolume; 

        // play Sound
        audioSource.Play();

        // get length of sound FX clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength); 


    }

    public void TestSFX() 
    {
        PlaySoundFXClip("SlashSword", transform); 
    }
}
