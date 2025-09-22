using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameStateManager gameStateManager;

    public enum SceneNames 
    {
        Gameplay,
        MainMenu,
        TitleScreen
    }

    private SceneNames _sceneName;  // Don't think I will need it

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
        SceneManager.LoadScene(SceneNames.Gameplay.ToString());
        gameStateManager.ChangeState(GameStateManager.GameState.Gameplay_State); 
    }


    public void ChangeScene(SceneNames nameScene) 
    {
        SceneManager.LoadScene(nameScene.ToString()); 
        
    }


}
