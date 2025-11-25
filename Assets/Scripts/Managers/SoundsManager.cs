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
    [SerializeField] private AudioClip iceSpellClip; // The sound effect when the enemies are frozen
    [SerializeField] private AudioClip windSlashClip; // The sound effect when the player unleash a wind attack    
    [SerializeField] private AudioClip startGameClip; // The sound when you start the gameplay
    [SerializeField] private AudioClip buttonSoundClip; // the sound when you press a button
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
            case "IceSpell":
                _audioSource.clip = iceSpellClip;
                break;
            case "WindSlash":
                _audioSource.clip = windSlashClip;
                break;
            case "StartGame":
                _audioSource.clip = startGameClip;
                break;
            case "ButtonPressed":
                _audioSource.clip = buttonSoundClip;
                break; 
        }
        

    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    

    public void PlaySoundFXClip(string clipName) 
    {
        // Spawn the gameObject, in this case is child of this manager
        AudioSource audioSource = Instantiate(soundFXObject, Vector3.zero, Quaternion.identity, transform);

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
        PlaySoundFXClip("SlashSword"); 
    }
       
}
