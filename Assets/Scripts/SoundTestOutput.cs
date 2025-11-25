using UnityEngine;
using UnityEngine.EventSystems;

public class SoundTestOutput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool changeSFXVolume = false;
    private SoundsManager soundManager; 


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
            soundManager.PlaySoundFXClip("SlashSword");
            changeSFXVolume = false;
        }
    }

    private void Awake()
    {
        soundManager = FindFirstObjectByType<SoundsManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerSoundTest() 
    {
       
    }
}
