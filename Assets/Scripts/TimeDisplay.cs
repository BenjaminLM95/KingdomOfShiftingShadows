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
        
        DisplayInfo();

    }

    public void DisplayInfo() 
    {
        if (dayNightManager != null) 
        {
            cycleText = dayNightManager.cycle.ToString();
            dayCount = dayNightManager.dayCount; 
        }

        timeDisplayClock.text = cycleText + " - " + "Day: " + dayCount + " - " + dayNightManager.GetTimeString(); 
    }
}
