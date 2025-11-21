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

    private SceneNames _sceneName;  // Don't think I will need it

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
        soundManager.PlaySoundFXClip("ButtonPressed", transform);
        SceneManager.LoadScene(SceneNames.MainMenu.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Menu_State);
        _musicManager.PlayMusic(true, "mainmenu"); 
    }

    public void ChangeToGameplay() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed", transform);
        SceneManager.LoadScene(SceneNames.Gameplay.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State);
        playerHealth.healthSystem.resetStats();
        playerController.SetStartingPosition();
        soundManager.PlaySoundFXClip("StartGame", transform);
        _musicManager.PlayMusic(true, "Gameplay");
    }

    public void ResumeGamePlay() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed", transform);
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State);         
    }

    public void ChangeToPause() 
    {
        gameStateManager.ChangeState(GameStateManager.GameState.Paused); 
    }

    public void ChangeToUpgrade() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed", transform);
        ChangeScene(SceneNames.UpgradeScene); 
        gameStateManager.ChangeState(GameStateManager.GameState.Upgrade);
        upgradeManager.DisplayItemsOnScreen(); 


    }

    public void GoToSettings() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed", transform);
        gameStateManager.ChangeState(GameStateManager.GameState.Settings);
        
    }

    public void InGameSetting() 
    {
        gameStateManager.ChangeState(GameStateManager.GameState.Settings); 
    }

    public void BackState() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed", transform);
        gameStateManager.ChangeState(gameStateManager.GivePreviousGameState()); 
    }

    public void StartNewGame() 
    {        
        ChangeToGameplay();
        StartingValues(); 
             

    }

    public void StartingValues() 
    {
        playerController.ResetValues();
        upgradeManager.RestartUpgrade();
    }

    public void GoToIntroduction() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed", transform);
        ChangeScene(SceneNames.Introduction);
        gameStateManager.ChangeState(GameStateManager.GameState.Introduction);
        
    }
        
    public void GoToCredits() 
    {
        soundManager.PlaySoundFXClip("ButtonPressed", transform);
        ChangeScene(SceneNames.Credits);
        gameStateManager.ChangeState(GameStateManager.GameState.Credit); 
    }

    public void ChangeScene(SceneNames nameScene) 
    {
        SceneManager.LoadScene(nameScene.ToString()); 
        
    }

    IEnumerator BeingGameplay() 
    {
        
        soundManager.PlaySoundFXClip("StartGame", transform);
        yield return new WaitForSeconds(1);
        ChangeToGameplay(); 
        StartingValues();
    }


    public void ExitGame() 
    {
        Application.Quit();
    }

    

}
