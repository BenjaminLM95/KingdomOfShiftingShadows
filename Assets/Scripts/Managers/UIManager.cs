using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreenUI;
    public GameObject menuUI;
    public GameObject gameplayUI;
    public GameObject pauseUI;
    public GameObject upgradeUI;
    public GameObject settingsUI;
    public GameObject introductionUI;
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
    
    public void EnableUpgradeUI() 
    {
        DisableAllUI();
        upgradeUI.gameObject.SetActive(true);
    }

    public void EnableSettingsUI() 
    {
        DisableAllUI();
        settingsUI.gameObject.SetActive(true); 
    }

    public void EnableIntroductionUI() 
    {
        DisableAllUI();
        introductionUI.gameObject.SetActive(true);
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
    }
}
