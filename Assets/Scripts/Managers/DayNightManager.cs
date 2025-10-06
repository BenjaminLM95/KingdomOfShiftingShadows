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
    public float totalTime;
    public float currentTime;    
    private int timeSpeed;
    private int initialHour;
    public int dayCount; 
    [SerializeField] private float dayDuration = 30f;
    [SerializeField] private float minuteDuration = 60f;

    [Header("Day and Night System")]
    public bool isDay;
    public DiurnalCycle cycle;
    [SerializeField] private int currentHour;
    [SerializeField] private int previousHour;
    private bool nextDay = false; 


    [Header("Day and Night Background Images")]
    public GameObject dayImg;
    public GameObject nightImg; 

    private EnemyManager enemyManager;
    


    private void Awake()
    {
        enemyManager = FindFirstObjectByType<EnemyManager>();
        InitialDaySetup();
        currentHour = (int)GetHour(); 
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime * 0.5f;

        currentTime = (initialHour + totalTime) % dayDuration;
        
        
        CycleChange(); 

        if(currentHour != (int)GetHour()) 
        {
            previousHour = currentHour;
            currentHour = (int)GetHour(); 
        }

        if(previousHour == 23 && currentHour == 0 && !nextDay) 
        {          
                dayCount++;
                nextDay = true;           
            
        }

        if (previousHour != 23 && currentHour != 0 && nextDay)
            nextDay = false; 



    }

    public void InitialDaySetup() 
    {
        initialHour = 8;
        dayCount = 1; 
        cycle = DiurnalCycle.Day;
        isDay = true;
        setDayImg();
    }

    public void CycleChange() 
    {
        if(GetHour() >= 6 && GetHour() < 18 && !isDay) 
        {            
            isDay = true;
            cycle = DiurnalCycle.Day;
            setDayImg();
            enemyManager.DestroyNightEnemies();
        }
        else if(GetHour() >= 18 && isDay) 
        {
            Debug.Log("Is Night"); 
            isDay = false;
            cycle = DiurnalCycle.Night; 
            setNightImg();
        }
    }

    public float GetHour() 
    {
        return currentTime * 24 / dayDuration;
    }

    public float GetMinutes() 
    {
        return (currentTime * 24 * minuteDuration / dayDuration) % 60; 
    }

    public string GetTimeString() 
    {
        return Mathf.FloorToInt(GetHour()).ToString("00") + " : " + Mathf.FloorToInt(GetMinutes()).ToString("00"); 
    }


    public void DisableImages() 
    {
        dayImg.gameObject.SetActive(false);
        nightImg.gameObject.SetActive(false);
    }

    public void setDayImg() 
    {
        DisableImages();
        dayImg.gameObject.SetActive(true);
    }

    public void setNightImg() 
    {
        DisableImages();
        nightImg.gameObject.SetActive(true);
    }
}
