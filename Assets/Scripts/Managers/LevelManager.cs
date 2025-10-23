using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameStateManager gameStateManager;
    public PlayerHealth playerHealth;
    public PlayerController playerController;

    public int playerCurrency; 

    public enum SceneNames 
    {
        Gameplay,
        MainMenu,
        TitleScreen,        
        UpgradeScene,
        Settings,
        Introduction


    }

    private SceneNames _sceneName;  // Don't think I will need it

    [Header("Reference")]
    [SerializeField] private MusicManager _musicManager;

    private void Start()
    {
        _musicManager = FindFirstObjectByType<MusicManager>();
        ChangeToTitleScreen();
        
    }

    public void ChangeToTitleScreen() 
    {
        SceneManager.LoadScene(SceneNames.TitleScreen.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Title_State);
    }

    public void ChangeToMainMenu() 
    {
        SceneManager.LoadScene(SceneNames.MainMenu.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Menu_State);
        _musicManager.PlayMusic(true, "mainmenu"); 
    }

    public void ChangeToGameplay() 
    {
        SceneManager.LoadScene(SceneNames.Gameplay.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State);
        playerHealth.healthSystem.resetStats();
        playerController.SetStartingPosition(); 
        _musicManager.PlayMusic(true, "Gameplay");
    }

    public void ResumeGamePlay() 
    {
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State); 
        
    }

    public void ChangeToPause() 
    {
        gameStateManager.ChangeState(GameStateManager.GameState.Paused); 
    }

    public void ChangeToUpgrade() 
    {
        ChangeScene(SceneNames.UpgradeScene); 
        gameStateManager.ChangeState(GameStateManager.GameState.Upgrade);
       
    }

    public void GoToSettings() 
    {
        ChangeScene(SceneNames.Settings);
        gameStateManager.ChangeState(GameStateManager.GameState.Settings); 
    }

    public void GoToIntroduction() 
    {
        ChangeScene(SceneNames.Introduction);
        gameStateManager.ChangeState(GameStateManager.GameState.Introduction);
        
    }
        


    public void ChangeScene(SceneNames nameScene) 
    {
        SceneManager.LoadScene(nameScene.ToString()); 
        
    }

    public void ExitGame() 
    {
        Application.Quit();
    }


}
