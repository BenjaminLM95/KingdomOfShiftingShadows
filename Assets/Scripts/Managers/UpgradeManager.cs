using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Upgrades gameUpgrades;
    

    public Upgrade nextSwordUpgrade;
    public Upgrade nextHealthUpgrade;
    public Upgrade nextSpeedUpgrade;

    public Upgrade currentSwordUpgrade = null;
    public Upgrade currentHealthUpgrade = null;
    public Upgrade currentSpeedUpgrade = null;

    public bool isSwordUpgrade;
    public bool isHealthUpgrade;
    public bool isSpeedUpgrade;

    public int swordIndex = 0;
    public int healthIndex = 0;
    public int speedIndex = 0;

    public int playerCurrency {  get; private set; }
    private SoundsManager soundManager; 

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager = FindFirstObjectByType<SoundsManager>(); 
        gameUpgrades = new Upgrades();
       

        SetAllUpgrades(); 

        nextSwordUpgrade = gameUpgrades.swordUpgrades[swordIndex]; 
        nextHealthUpgrade = gameUpgrades.healthUpgrades[healthIndex];
        nextSpeedUpgrade = gameUpgrades.speedUpgrades[speedIndex];        

        isSwordUpgrade = false;
        isHealthUpgrade = false;
        isSpeedUpgrade = false;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void SetAllUpgrades() 
    {
        gameUpgrades.AddSwordUpgrades();
        gameUpgrades.AddHealthUpgrades();
        gameUpgrades.AddSpeedUpgrades(); 

    }

    public Upgrade ChangeTier(Upgrades listUpgrades, Upgrade _upgrade) 
    {        

        switch (_upgrade.type) 
        {
            case typeUpgrade.Sword:
                if (swordIndex < listUpgrades.swordUpgrades.Count - 1) 
                {                    
                    currentSwordUpgrade = listUpgrades.swordUpgrades[swordIndex];
                    swordIndex++;
                    _upgrade = listUpgrades.swordUpgrades[swordIndex];                    
                }
                
                break; 
            case typeUpgrade.Health:
                if (healthIndex < listUpgrades.healthUpgrades.Count - 1)
                {
                    currentHealthUpgrade = listUpgrades.healthUpgrades[healthIndex];
                    healthIndex++;
                    _upgrade = listUpgrades.healthUpgrades[healthIndex];
                }
                
                break; 
            case typeUpgrade.Speed: 
                {
                    if (speedIndex < listUpgrades.speedUpgrades.Count - 1)
                    {
                        currentSpeedUpgrade = listUpgrades.speedUpgrades[speedIndex];
                        speedIndex++;
                        _upgrade = listUpgrades.speedUpgrades[speedIndex];
                    }
                }
                
                break;

        }

        soundManager.PlaySoundFXClip("BuySound", transform); 
        return _upgrade; 
    }

        

    public void GetSwordUpgrade() 
    {
        if (playerCurrency >= nextSwordUpgrade.cost)
        {
            playerCurrency -= nextSwordUpgrade.cost;

            if (!isSwordUpgrade)
                isSwordUpgrade = true;



            nextSwordUpgrade = ChangeTier(gameUpgrades, nextSwordUpgrade);
            Debug.Log(nextSwordUpgrade.description + " , " + nextSwordUpgrade.cost + " , " + nextSwordUpgrade.value);
        }
    }

    public void GetHealthUpgrade() 
    {
        if (playerCurrency >= nextHealthUpgrade.cost)
        {
            playerCurrency -= nextHealthUpgrade.cost;

            if (!isHealthUpgrade)
                isHealthUpgrade = true;


            nextHealthUpgrade = ChangeTier(gameUpgrades, nextHealthUpgrade);            
            Debug.Log(nextHealthUpgrade.description + " , " + nextHealthUpgrade.cost + " , " + nextHealthUpgrade.value);
        }
    }

    public void GetSpeedUpgrade() 
    {
        if (playerCurrency >= nextSpeedUpgrade.cost)
        {
            playerCurrency -= nextSpeedUpgrade.cost;

            if (!isSpeedUpgrade)
                isSpeedUpgrade = true;


            nextSpeedUpgrade = ChangeTier(gameUpgrades, nextSpeedUpgrade);
            Debug.Log(nextSpeedUpgrade.description + " , " + nextSpeedUpgrade.cost + " , " + nextSpeedUpgrade.value);
        }
    }        

    public void ObtainingMoneyReward(int currency) 
    {
        playerCurrency += currency;
    }


    public void RestartUpgrade() 
    {        
        currentSwordUpgrade = null;
        currentHealthUpgrade = null;
        currentSpeedUpgrade = null;
        swordIndex = 0;
        healthIndex = 0;
        speedIndex = 0;
        isSwordUpgrade = false;
        isHealthUpgrade = false;
        isSpeedUpgrade = false;
        playerCurrency = 0;
        nextSwordUpgrade = gameUpgrades.swordUpgrades[0];
        nextHealthUpgrade = gameUpgrades.healthUpgrades[0];
        nextSpeedUpgrade = gameUpgrades.speedUpgrades[0];
    }
       

}
