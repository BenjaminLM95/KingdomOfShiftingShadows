using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static GameStateManager;

public class KingdomGate : MonoBehaviour
{
    public bool gameOver;// { get; private set; }
    public bool gameWin;// { get; private set; }
    //public GameObject gameOverText;
    //public GameObject gameWinText;
    [SerializeField] private int dayMax;


    [Header("Reference")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ResultInformation resultInfo;
    [SerializeField] private DayNightManager dayNightManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private GameStateManager gameStateManager;


    private void Awake()
    {
        gameOver = false; 
        gameWin = false;
        playerController = FindFirstObjectByType<PlayerController>();        
        resultInfo = FindFirstObjectByType<ResultInformation>();
        dayNightManager = FindFirstObjectByType<DayNightManager>();
        musicManager = FindFirstObjectByType<MusicManager>();
        gameStateManager = FindFirstObjectByType<GameStateManager>();
        dayMax = 6; 
    }

    private void Update()
    {
        if(!gameOver && playerController.isDead && !gameWin) 
        {
            SetGameOver();
        }

        if(dayNightManager.dayCount >= dayMax && dayNightManager.GetHour() >= 6 && !gameOver && !gameWin) 
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
        gameStateManager.ChangeState(GameState.Result); 
        musicManager.PlayMusic(true, "Result"); 

        if(resultInfo == null)
            resultInfo = FindFirstObjectByType<ResultInformation>();

        resultInfo.DisplayResult(); 
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
