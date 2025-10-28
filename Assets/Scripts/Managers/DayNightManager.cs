using TMPro;
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
    public GameObject sunImg;
    public GameObject moonImg; 

    private EnemyManager enemyManager;
    public TextMeshProUGUI dayNotificationText; 

    [Header("References")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TimeDisplay timeDisplay;
    [SerializeField] private NewGameScene newGameScene; 



    private void Awake()
    {
        enemyManager = FindFirstObjectByType<EnemyManager>();
        levelManager = FindFirstObjectByType<LevelManager>();
        timeDisplay = FindFirstObjectByType<TimeDisplay>();
        newGameScene = FindFirstObjectByType<NewGameScene>();
        InitialDaySetup();
        currentHour = (int)GetHour();

        

    }

    // Update is called once per frame
    void Update()
    {
        if (newGameScene.isStarted)
        {
            totalTime += Time.deltaTime * 0.5f;

            currentTime = (initialHour + totalTime) % dayDuration;
        }
        
        
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

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(levelManager.gameStateManager.currentGameState == GameStateManager.GameState.Gameplay_State) 
            {
                levelManager.ChangeToPause(); 
            }
            else if(levelManager.gameStateManager.currentGameState == GameStateManager.GameState.Paused) 
            {
                levelManager.ResumeGamePlay(); 
            }
        }

        DisplayTime(); 

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
            DayNotification(); 
            enemyManager.DestroyNightEnemies();
            Invoke("DayTextDisapear", 2f); 
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
        sunImg.gameObject.SetActive(false);
        moonImg.gameObject.SetActive(false); 
    }

    public void setDayImg() 
    {
        DisableImages();
        dayImg.gameObject.SetActive(true);
        sunImg.gameObject.SetActive(true); 
    }

    public void setNightImg() 
    {
        DisableImages();
        nightImg.gameObject.SetActive(true);
        moonImg.gameObject.SetActive(true); 
    }

    public void DisplayTime() 
    {
        timeDisplay.dayCount = dayCount; 
        timeDisplay.timeText = "Day: " + dayCount + " - " + GetTimeString();
    }

    public void DayNotification() 
    {
        dayNotificationText.gameObject.SetActive(true);
        dayNotificationText.text = DisplayDayNotificationText(dayCount); 
    }

    public string DisplayDayNotificationText(int nDay) 
    {
        string returningText; 

        switch (nDay) 
        {
            case 1:
                returningText = "1st day of 6";
                break;
            case 2:
                returningText = "2nd day of 6";
                break;
            case 3:
                returningText = "3rd day of 6";
                break;
            case 4:
                returningText = "4th day of 6";
                break;
            case 5:
                returningText = "5th day of 6";
                break;
            case 6:
                returningText = "6th day of 6";
                break;
            default:
                returningText = "Another Day passed";
                break;

        }

        return returningText; 
    }

    public void DayTextDisapear() 
    {
        dayNotificationText.gameObject.SetActive(false);
    }
}
