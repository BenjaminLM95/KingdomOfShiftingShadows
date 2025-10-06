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
    [SerializeField] private float dayDuration = 30f; 

    [Header("Day and Night System")]
    public bool isDay;
    public DiurnalCycle cycle;


    [Header("Day and Night Background Images")]
    public GameObject dayImg;
    public GameObject nightImg; 

    private EnemyManager enemyManager;
    


    private void Awake()
    {
        enemyManager = FindFirstObjectByType<EnemyManager>();
        InitialDaySetup();     
         
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        currentTime = (initialHour + totalTime) % dayDuration;

        //Debug.Log(currentTime); 
        
        CycleChange(); 
        
    }

    public void InitialDaySetup() 
    {
        initialHour = 8;         
        cycle = DiurnalCycle.Day;
        isDay = true;
        setDayImg();
    }

    public void CycleChange() 
    {
        if(GetHour() >= 6 && GetHour() < 18 && !isDay) 
        {
            //Debug.Log(GetHour());
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
        return (currentTime * 24 * 60 / dayDuration) % 60; 
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
