using UnityEngine;

public class MenuTransition : MonoBehaviour
{
    
    public GameObject dayMenuImg;
    public GameObject nightMenuImg;
    private bool day = true;    
    private float cooldown = 10f;
    private float timerCount = 0; 
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetDayMenuImage();
        timerCount = 0;
        Debug.Log("Started"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(timerCount > cooldown) 
        {
            Debug.Log("Change");
            if (day) 
            {
                SetNightImage();                
                timerCount = Time.time + cooldown; 
            }
            else 
            {
                SetDayMenuImage();
                timerCount = Time.time + cooldown;
            }
            timerCount = 0; 
        }

        timerCount += Time.deltaTime;
        Debug.Log(timerCount + " , " + Time.deltaTime);
    }

    void SetDayMenuImage() 
    {
        nightMenuImg.SetActive(false);
        dayMenuImg.SetActive(true);
        day = true; 
    }

    void SetNightImage() 
    {
        dayMenuImg.SetActive(false); 
        nightMenuImg.SetActive(true);
        day = false; 
    }

}
