using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreenUI;
    public GameObject menuUI;
    public GameObject gameplayUI;
    public GameObject pauseUI; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void EnableTitleScreenUI() 
    {
        DisableAllUI();
        titleScreenUI.SetActive(true);
    }

    public void EnableMenuUI() 
    {
        DisableAllUI();
        menuUI.gameObject.SetActive(true); 
    }

    public void EnableGameplayUI() 
    {
        DisableAllUI();
        gameplayUI.gameObject.SetActive(true);
    }

    public void EnablePauseUI() 
    {
        DisableAllUI();
        pauseUI.gameObject.SetActive(true);
    }

    public void DisableAllUI() 
    {
        menuUI.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(false);
        titleScreenUI.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(false);
    }
}
