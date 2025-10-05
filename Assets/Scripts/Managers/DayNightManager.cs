using UnityEngine;

public enum DiurnalCycle 
{
    Day,
    Night,
    None
}

public class DayNightManager : MonoBehaviour
{

    [Header("Time Management")]
    public float currentTime;
    public int dayCount;
    public int hourCount;
    public int minuteCount;
    private int timeSpeed;

    [Header("Day and Night System")]
    public bool isDay;
    public DiurnalCycle cycle; 
    


    private void Awake()
    {
        InitialDaySetup();
        Debug.Log(hourCount); 
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * timeSpeed;

        hourCount = (int)(currentTime / 3600f);
        minuteCount = (int)(currentTime % 60f); 

        if(hourCount >= 24) 
        {
            dayCount++; 
            currentTime = 0;
        }

        CycleChange(); 
        
    }

    public void InitialDaySetup() 
    {
        currentTime = 0;
        timeSpeed = 60;
        dayCount = 1;
        hourCount = 6;
        minuteCount = 0;
        cycle = DiurnalCycle.Day;
        isDay = true;
    }

    public void CycleChange() 
    {
        if(hourCount >= 6 && hourCount < 18 && !isDay) 
        {
            isDay = true;
            cycle = DiurnalCycle.Day; 
        }
        else if(hourCount >= 18 && hourCount < 6 && isDay) 
        {
            isDay = false;
            cycle = DiurnalCycle.Night; 
        }
    }
}
