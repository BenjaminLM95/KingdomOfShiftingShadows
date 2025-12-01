using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static GameStateManager;

public class KingdomGate : MonoBehaviour
{
    public bool gameOver;
    public bool gameWin;
    [SerializeField] private int dayMax;


    [Header("Reference")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ResultInformation resultInfo;
    [SerializeField] private DayNightManager dayNightManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private SoundsManager soundManager; 
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private PlayerHealth playerHealth;
    


    private void Awake()
    {
        gameOver = false; 
        gameWin = false;
        playerController = FindFirstObjectByType<PlayerController>();        
        resultInfo = FindFirstObjectByType<ResultInformation>();
        dayNightManager = FindFirstObjectByType<DayNightManager>();
        musicManager = FindFirstObjectByType<MusicManager>();
        soundManager = FindFirstObjectByType<SoundsManager>(); 
        gameStateManager = FindFirstObjectByType<GameStateManager>();
        playerHealth = FindFirstObjectByType<PlayerHealth>(); 
        dayMax = 6; 
    }

    private void Update()
    {
        if(playerController.playerState == PlayerState.Death && !gameOver) 
        {
            musicManager.ChangeSpeed(1); 
            playerHealth.vulnerability();            
            gameOver = true; 
            Invoke("SetGameOver", 1f);             
        }

        if(dayNightManager.dayCount >= dayMax && dayNightManager.GetHour() >= 6 && !gameOver && !gameWin) 
        {
            musicManager.ChangeSpeed(1);
            SetGameWin(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy")) 
        {
            if (!gameOver)
            {
                musicManager.ChangeSpeed(1);
                playerController.playerState = PlayerState.Death;
                playerHealth.vulnerability();                
                Invoke("SetGameOver", 0.25f);
            }
        }
    }

    private void SetGameOver() 
    {
        Debug.Log("Game Over");        
        gameOver = true;
        musicManager.PlayMusic(true, "Result");
        gameStateManager.ChangeState(GameState.Result);         
        playerController.FreezePlayer();

        if(resultInfo == null)
            resultInfo = FindFirstObjectByType<ResultInformation>();
                
        resultInfo.DisplayResult();
        playerController.gameObject.SetActive(false);
        gameOver = false; 
        Time.timeScale = 0f;
    }

    private void SetGameWin() 
    {
        gameWin = true;
        Debug.Log("Game Win");
        musicManager.PlayMusic(true, "WinningScreen");
        gameStateManager.ChangeState(GameState.WinScreen);       
        Time.timeScale = 0f;
    }

    
}
