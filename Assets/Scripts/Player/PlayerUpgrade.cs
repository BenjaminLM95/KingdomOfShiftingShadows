using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public PlayerHealth playerHealth;
    private UpgradeManager upgradeManager;

    private void Awake()
    {
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        CheckForUpgrades();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForUpgrades();
    }

    void CheckForUpgrades() 
    {
        if (upgradeManager.isSwordUpgrade && upgradeManager.currentSwordUpgrade != null)
        {
            if (playerController.upgradeSwordPower != upgradeManager.currentSwordUpgrade.value)
            {
                playerController.upgradeSwordPower = upgradeManager.currentSwordUpgrade.value;                
                playerController.UpdatingSwordMight();
            }
        }

        if (upgradeManager.isSpeedUpgrade && upgradeManager.currentSpeedUpgrade != null)
        {
            if (playerController.upgradeSpeedMove != upgradeManager.currentSpeedUpgrade.value)
            {
                playerController.upgradeSpeedMove = upgradeManager.currentSpeedUpgrade.value;                
                playerController.UpdatingSpeed();
            }
        }

        if (upgradeManager.isHealthUpgrade && upgradeManager.currentHealthUpgrade != null)
        {
            if (playerHealth.upgradeHealhValue != upgradeManager.currentHealthUpgrade.value)
            {
                playerHealth.upgradeHealhValue = upgradeManager.currentHealthUpgrade.value;
                playerHealth.UpdatingHealthUpgrade();
            }
        }

        if(upgradeManager.isKnockbackUpgrade && upgradeManager.currentKnockbackUpgrade != null) 
        {
            if(playerController.upgradeKnockback != upgradeManager.currentKnockbackUpgrade.value) 
            {
                playerController.upgradeKnockback = upgradeManager.currentKnockbackUpgrade.value;
                playerController.UpdatingKnockback(); 
            }
        }
    }
}
