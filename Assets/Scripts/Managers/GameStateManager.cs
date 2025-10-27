using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public UIManager uiManager; 

    public enum GameState 
    {
        Title_State,
        Menu_State,
        Gameplay_State,
        Paused,
        Upgrade,
        Settings,
        Introduction,
        Credit,
        Result,
        WinScreen,
        None
    }
    // Property to store the current game state, accessible publicly but modifiable only within this class
    public GameState currentGameState { get; private set; }

    // Debugging variables to store the current and last game state as strings for easier debugging in the Inspector
    [Header("Game State")]
    [SerializeField] private string currentStateDebug;
    [SerializeField] private string lastStateDebug; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState) 
    {
        lastStateDebug = currentGameState.ToString();

        currentGameState = newState; 

        HandleStateChange(newState);

        currentStateDebug = newState.ToString();
    }

    public GameState GivePreviousGameState() 
    {

        string prevGStateName = lastStateDebug;
        GameState returningGameState; 

        switch (prevGStateName) 
        {
            case "Title_State":
                returningGameState = GameState.Title_State;                
                break;
            case "Menu_State":
                returningGameState = GameState.Menu_State;
                break;
            case "Gameplay_State":
                returningGameState = GameState.Gameplay_State;                
                break;
            case "Paused":
                returningGameState = GameState.Paused;                
                break;
            case "Upgrade":
                returningGameState = GameState.Upgrade;                
                break;
            case "Settings":
                returningGameState = GameState.Settings;                
                break;
            case "Introduction":
                returningGameState = GameState.Introduction;                
                break;
            case "Credit":
                returningGameState = GameState.Credit;
                Cursor.visible = true;
                break;
            case "Result":
                returningGameState = GameState.Result;               
                break;
            case "WinScreen":
                returningGameState = GameState.WinScreen;               
                break;
            default:
                returningGameState = GameState.None;
                break;

        }

        return returningGameState; 

    }

    public void HandleStateChange(GameState state) 
    {
        // TODO
        switch (state) 
        {
            case GameState.Title_State:                
                uiManager.EnableTitleScreenUI();
                Cursor.visible = false;
                Time.timeScale = 0f; // <= Check this later 
                break;
            case GameState.Menu_State:                
                uiManager.EnableMenuUI();
                Cursor.visible = true;
                Time.timeScale = 1f;  // <= Check this later 
                break;
            case GameState.Gameplay_State:                
                uiManager.EnableGameplayUI();
                Cursor.visible = false;
                Time.timeScale = 1f;  // the time resumes
                break;
            case GameState.Paused:               
                uiManager.EnablePauseUI();
                Cursor.visible = true;
                Time.timeScale = 0f;  // The game paused, so the time stops
                break;
            case GameState.Upgrade:                
                uiManager.EnableUpgradeUI();
                Cursor.visible = true;
                Time.timeScale = 0f;
                break;
            case GameState.Settings:                
                uiManager.EnableSettingsUI();
                Cursor.visible = true;
                Time.timeScale = 0f;
                break;
            case GameState.Introduction:                
                uiManager.EnableIntroductionUI();
                Cursor.visible = true;
                Time.timeScale = 0f;
                break;
            case GameState.Credit:
                uiManager.EnableCreditsUI();
                Cursor.visible = true;
                Time.timeScale = 0f;
                break;
            case GameState.Result:
                uiManager.EnableResultsUI();
                Cursor.visible = true;
                Time.timeScale = 0f;
                break;
            case GameState.WinScreen:
                uiManager.EnableGameWinUI();
                Cursor.visible = true;
                Time.timeScale = 0f;
                break; 

        }
    }

}
