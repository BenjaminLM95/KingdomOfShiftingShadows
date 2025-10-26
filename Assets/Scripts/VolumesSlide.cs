using UnityEngine;
using UnityEngine.UI;

public class VolumesSlide : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    public float musicVolume;
    public float soundVolume;

    [SerializeField] private SoundsManager soundManager;
    [SerializeField] private MusicManager musicManager;

    private void Awake()
    {
        musicManager = FindFirstObjectByType<MusicManager>();
        soundManager = FindFirstObjectByType<SoundsManager>();

        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSlider.value = 1f;
        soundSlider.value = 1f; 
        musicVolume = musicSlider.value;
        soundVolume = soundSlider.value;
        musicManager.SetVolume(musicVolume);
        soundManager.SetVolume(soundVolume);
        soundManager.sfxVolume = soundVolume; 
    }

    // Update is called once per frame
    void Update()
    {
        if(musicVolume != musicSlider.value) 
        {
            musicVolume = musicSlider.value;
            musicManager.SetVolume(musicVolume);
            soundManager.sfxVolume = soundVolume;
        }

        if(soundVolume != soundSlider.value) 
        {
            soundVolume = soundSlider.value;
            soundManager.SetVolume(soundVolume);
            soundManager.sfxVolume = soundVolume;
        }
    }

    
}
