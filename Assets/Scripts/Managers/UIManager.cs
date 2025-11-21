using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("All the UIs objects")]
    public GameObject titleScreenUI;
    public GameObject menuUI;
    public GameObject gameplayUI;
    public GameObject pauseUI;
    public GameObject upgradeUI;
    public GameObject settingsUI;
    public GameObject introductionUI;
    public GameObject creditUI;
    public GameObject resultUI;
    public GameObject gameWinUI;

    [Header("All other objects ")]
    public GameObject player; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void EnableTitleScreenUI() 
    {
        DisableAllUI();
        titleScreenUI.SetActive(true);
        player.gameObject.SetActive(false);
    }

    public void EnableMenuUI() 
    {
        DisableAllUI();
        menuUI.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
    }

    public void EnableGameplayUI() 
    {
        DisableAllUI();
        gameplayUI.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
    }

    public void EnablePauseUI() 
    {
        DisableAllUI();
        pauseUI.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
    }
    
    public void EnableUpgradeUI() 
    {
        DisableAllUI();
        upgradeUI.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
    }

    public void EnableSettingsUI() 
    {
        DisableAllUI();
        settingsUI.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
    }

    public void EnableIntroductionUI() 
    {
        DisableAllUI();
        introductionUI.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
    }

    public void EnableCreditsUI() 
    {
        DisableAllUI();
        creditUI.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
    }

    public void EnableResultsUI() 
    {
        DisableAllUI();
        resultUI.gameObject.SetActive(true);
        player.gameObject.SetActive(true); 
    }

    public void EnableGameWinUI() 
    {
        DisableAllUI();
        gameWinUI.gameObject.SetActive(true);
    }


    public void DisableAllUI() 
    {
        menuUI.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(false);
        titleScreenUI.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(false);
        upgradeUI.gameObject.SetActive(false);
        settingsUI.gameObject.SetActive(false); 
        introductionUI.gameObject.SetActive(false);
        creditUI.gameObject.SetActive(false);
        resultUI.gameObject.SetActive(false);
        gameWinUI.gameObject.SetActive(false);        
    }
}
