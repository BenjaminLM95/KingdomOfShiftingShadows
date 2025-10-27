using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumesSlide : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider musicSlider;
    public Slider soundSlider;

    public float musicVolume;
    public float soundVolume;

    [SerializeField] private SoundsManager soundManager;
    [SerializeField] private MusicManager musicManager;

    private bool changeSFXVolume = false;

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

    public void OnPointerDown(PointerEventData eventData)
    {
        changeSFXVolume = true;
        Debug.Log("PointDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("PointUp");
        if (changeSFXVolume)
        {
            soundManager.PlaySoundFXClip("SlashSword", transform);
            changeSFXVolume = false;
        }
    }

}
