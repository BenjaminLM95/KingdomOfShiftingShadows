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

    public void HandleStateChange(GameState state) 
    {
        // TODO
        switch (state) 
        {
            case GameState.Title_State:
                Debug.Log("Switch to Title Screen");
                uiManager.EnableTitleScreenUI();
                Time.timeScale = 0f; // <= Check this later 
                break;
            case GameState.Menu_State:
                Debug.Log("Switched to MainMenu State");
                uiManager.EnableMenuUI();
                Time.timeScale = 0f;  // <= Check this later 
                break;
            case GameState.Gameplay_State:
                Debug.Log("Switch to Gameplay State");
                uiManager.EnableGameplayUI();
                Time.timeScale = 1f;  // the time resumes
                break;
            case GameState.Paused:
                Debug.Log("Switch to Pause State");
                uiManager.EnablePauseUI();
                Time.timeScale = 0f;  // The game paused, so the time stops
                break; 

        }
    }

}
