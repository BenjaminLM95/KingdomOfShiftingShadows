using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeDisplayClock;
    public string timeText; 

    [Header("Time Properties")]
    public int dayCount = 0;
    public int hourCount = 0;
    public int minuteCount = 0;   
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        DisplayInfo(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        DisplayInfo();

    }

    public void DisplayInfo() 
    {

        timeDisplayClock.text = timeText; 
    }
}
