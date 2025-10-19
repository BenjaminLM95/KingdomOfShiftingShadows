using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Upgrades gameUpgrades;
    public PlayerController playerController;

    public Upgrade currentSwordUpgrade;
    public Upgrade currentHealthUpgrade;
    public Upgrade currentSpeedUpgrade; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameUpgrades = new Upgrades();
        playerController = FindFirstObjectByType<PlayerController>();

        SetAllUpgrades(); 

        currentSwordUpgrade = gameUpgrades.swordUpgrades[0]; 
        currentHealthUpgrade = gameUpgrades.healthUpgrades[0];
        currentSpeedUpgrade = gameUpgrades.speedUpgrades[0];

        Debug.Log(currentSwordUpgrade.description + " , " + currentSwordUpgrade.cost + " , " + currentSwordUpgrade.value);
        Debug.Log(currentHealthUpgrade.description + " , " + currentHealthUpgrade.cost + " , " + currentHealthUpgrade.value);
        Debug.Log(currentSpeedUpgrade.description + " , " + currentSpeedUpgrade.cost + " , " + currentSpeedUpgrade.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            currentSwordUpgrade = ChangeTier(gameUpgrades, currentSwordUpgrade);
            Debug.Log(currentSwordUpgrade.description + " , " + currentSwordUpgrade.cost + " , " + currentSwordUpgrade.value);
        }
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            currentHealthUpgrade = ChangeTier(gameUpgrades, currentHealthUpgrade);
            Debug.Log(currentHealthUpgrade.description + " , " + currentHealthUpgrade.cost + " , " + currentHealthUpgrade.value);
        }
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            currentSpeedUpgrade =  ChangeTier(gameUpgrades, currentSpeedUpgrade);
            Debug.Log(currentSpeedUpgrade.description + " , " + currentSpeedUpgrade.cost + " , " + currentSpeedUpgrade.value);
        }

    }

    public void SetAllUpgrades() 
    {
        gameUpgrades.AddSwordUpgrades();
        gameUpgrades.AddHealthUpgrades();
        gameUpgrades.AddSpeedUpgrades(); 

    }

    public Upgrade ChangeTier(Upgrades listUpgrades, Upgrade currentUpgrade) 
    {
        int currentTier = currentUpgrade.tier;

        switch (currentUpgrade.type) 
        {
            case typeUpgrade.Sword:
                if (currentTier < listUpgrades.swordUpgrades.Count) 
                {
                    currentUpgrade = listUpgrades.swordUpgrades[currentTier++];                    
                }
                
                break; 
            case typeUpgrade.Health:
                if (currentTier < listUpgrades.healthUpgrades.Count)
                {
                    currentUpgrade = listUpgrades.healthUpgrades[currentTier++];
                }
                
                break; 
            case typeUpgrade.Speed: 
                {
                    if (currentTier < listUpgrades.speedUpgrades.Count)
                    {
                        currentUpgrade = listUpgrades.speedUpgrades[currentTier++];
                    }
                }
                
                break;

        }

        return currentUpgrade; 
    }

    public void GetSwordUpgrade() 
    {
        currentSwordUpgrade = ChangeTier(gameUpgrades, currentSwordUpgrade);
        Debug.Log(currentSwordUpgrade.description + " , " + currentSwordUpgrade.cost + " , " + currentSwordUpgrade.value);
    }

    public void GetHealthUpgrade() 
    {
        currentHealthUpgrade = ChangeTier(gameUpgrades, currentHealthUpgrade);
        Debug.Log(currentHealthUpgrade.description + " , " + currentHealthUpgrade.cost + " , " + currentHealthUpgrade.value);
    }

    public void GetSpeedUpgrade() 
    {
        currentSpeedUpgrade = ChangeTier(gameUpgrades, currentSpeedUpgrade);
        Debug.Log(currentSpeedUpgrade.description + " , " + currentSpeedUpgrade.cost + " , " + currentSpeedUpgrade.value);
    }



}
