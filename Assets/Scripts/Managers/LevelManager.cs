using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameStateManager gameStateManager;

    public enum SceneNames 
    {
        Gameplay,
        MainMenu,
        TitleScreen,
        TestScene
    }

    private SceneNames _sceneName;  // Don't think I will need it

    private void Start()
    {
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
    }

    public void ChangeToGameplay() 
    {
        SceneManager.LoadScene(SceneNames.TestScene.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State); 
    }

    public void ResumeGamePlay() 
    {
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State); 
    }

    public void ChangeToPause() 
    {
        gameStateManager.ChangeState(GameStateManager.GameState.Paused); 
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
