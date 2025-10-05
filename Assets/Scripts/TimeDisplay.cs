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
    [SerializeField] private string cycleText; 
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dayNightManager = FindFirstObjectByType<DayNightManager>();
        DisplayInfo(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (dayCount != dayNightManager.dayCount || hourCount != dayNightManager.hourCount || minuteCount != dayNightManager.minuteCount || cycleText != dayNightManager.cycle.ToString()) 
        {
            DisplayInfo(); 
        }
        
    }

    public void DisplayInfo() 
    {
        if (dayNightManager != null) 
        {
            dayCount = dayNightManager.dayCount;
            hourCount = dayNightManager.hourCount;
            minuteCount = dayNightManager.minuteCount;
            cycleText = dayNightManager.cycle.ToString();

            Debug.Log(hourCount); 
        }

        timeDisplayClock.text = cycleText + " - " + "Day: " + dayCount + "  HH" + hourCount + " MM" + minuteCount; 
    }
}
