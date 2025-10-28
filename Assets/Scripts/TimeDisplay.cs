using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeDisplayClock;
    [SerializeField] private DayNightManager dayNightManager;

    [Header("Time Properties")]
    [SerializeField] private int dayCount;
    [SerializeField] private int hourCount;
    [SerializeField] private int minuteCount;   
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dayNightManager = FindFirstObjectByType<DayNightManager>();
        DisplayInfo(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        DisplayInfo();

    }

    public void DisplayInfo() 
    {
        if (dayNightManager != null) 
        {            
            dayCount = dayNightManager.dayCount; 
        }

        timeDisplayClock.text = "Day: " + dayCount + " - " + dayNightManager.GetTimeString(); 
    }
}
