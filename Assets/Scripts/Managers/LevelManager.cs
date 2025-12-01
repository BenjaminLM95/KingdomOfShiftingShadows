using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameStateManager gameStateManager;
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    [SerializeField] private UpgradeManager upgradeManager;
    public UIUpgradeManager uiUpgradeManager;
    [SerializeField] private SoundsManager soundManager;
    

    public int playerCurrency; 

    public enum SceneNames 
    {
        Gameplay,
        MainMenu,
        TitleScreen,        
        UpgradeScene,
        Settings,
        Introduction,
        Credits

    }
    

    [Header("Reference")]
    [SerializeField] private MusicManager _musicManager;
    

    private void Start()
    {
        _musicManager = FindFirstObjectByType<MusicManager>();
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        soundManager = FindFirstObjectByType<SoundsManager>(); 
        ChangeToMainMenu();


    }

    public void ChangeToTitleScreen() 
    {
        SceneManager.LoadScene(SceneNames.TitleScreen.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Title_State);
    }

    public void ChangeToMainMenu() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed");
        _musicManager.ChangeSpeed(1); 
        SceneManager.LoadScene(SceneNames.MainMenu.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Menu_State);
        _musicManager.PlayMusic(true, "mainmenu"); 
    }

    public void ChangeToGameplay() 
    {
        //soundManager.PlaySoundFXClip("ButtonPressed");
        SceneManager.LoadScene(SceneNames.Gameplay.ToString());
        Debug.Log("Change to gameplay scene"); 
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State);
        playerHealth.healthSystem.resetStats();
        playerController.SetStartingPosition();        
        _musicManager.PlayMusic(true, "Gameplay");
    }

    public void ResumeGamePlay() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed");
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State);         
    }

    public void ChangeToPause() 
    {
        gameStateManager.ChangeState(GameStateManager.GameState.Paused); 
    }

    public void ChangeToUpgrade() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed");
        ChangeScene(SceneNames.UpgradeScene); 
        gameStateManager.ChangeState(GameStateManager.GameState.Upgrade);
        upgradeManager.DisplayItemsOnScreen(); 


    }

    public void GoToSettings() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed");
        gameStateManager.ChangeState(GameStateManager.GameState.Settings);
        
    }

    public void InGameSetting() 
    {
        gameStateManager.ChangeState(GameStateManager.GameState.Settings); 
    }

    public void BackState() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed");
        gameStateManager.ChangeState(gameStateManager.GivePreviousGameState()); 
    }

    public void StartNewGame() 
    {
        StartCoroutine(BeginGameplay());                  

    }

    public void StartingValues() 
    {
        playerController.ResetValues();
        upgradeManager.RestartUpgrade();
    }

    public void GoToIntroduction() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed");
        ChangeScene(SceneNames.Introduction);
        gameStateManager.ChangeState(GameStateManager.GameState.Introduction);
        
    }
        
    public void GoToCredits() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed");
        ChangeScene(SceneNames.Credits);
        gameStateManager.ChangeState(GameStateManager.GameState.Credit); 
    }

    public void ChangeScene(SceneNames nameScene) 
    {
        SceneManager.LoadScene(nameScene.ToString()); 
        
    }

    IEnumerator BeginGameplay() 
    {
        soundManager.PlaySoundFXClip("StartGame");       
        yield return new WaitForSecondsRealtime(1);
        upgradeManager.ClearInventory(); 
        ChangeToGameplay(); 
        StartingValues();
        yield return null; 
    }


    public void ExitGame() 
    {
        Application.Quit();
    }

    

}
