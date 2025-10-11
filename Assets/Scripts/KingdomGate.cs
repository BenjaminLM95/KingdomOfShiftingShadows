using UnityEngine;

public class KingdomGate : MonoBehaviour
{
    public bool gameOver { get; private set; }
    public GameObject gameOverText;
    public GameObject gameWinText;
    [SerializeField] private int dayMax; 


    [Header("Reference")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ResultInformation resultInfo;
    [SerializeField] private DayNightManager dayNightManager;


    private void Awake()
    {
        gameOver = false; 
        playerHealth = FindFirstObjectByType<PlayerHealth>();        
        resultInfo = FindFirstObjectByType<ResultInformation>();
        dayNightManager = FindFirstObjectByType<DayNightManager>();
        dayMax = 10; 
    }

    private void Update()
    {
        if(!gameOver && playerHealth.healthSystem.health <= 0) 
        {
            SetGameOver();
        }

        if(dayNightManager.dayCount >= dayMax) 
        {
            SetGameWin(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy")) 
        {
            if (!gameOver)
            {
                SetGameOver();
            }
        }
    }

    private void SetGameOver() 
    {
        Debug.Log("Game Over"); 
        gameOver = true;
        gameOverText.gameObject.SetActive(true);

        if(resultInfo == null)
            resultInfo = FindFirstObjectByType<ResultInformation>();

        resultInfo.DisplayResult(); 
        Time.timeScale = 0f;
    }

    private void SetGameWin() 
    {
        Debug.Log("Game Win");
        gameWinText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    
}
