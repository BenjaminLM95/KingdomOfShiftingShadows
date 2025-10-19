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

    public int playerCurrency {  get; private set; }     

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameUpgrades = new Upgrades();
       

        SetAllUpgrades(); 

        nextSwordUpgrade = gameUpgrades.swordUpgrades[0]; 
        nextHealthUpgrade = gameUpgrades.healthUpgrades[0];
        nextSpeedUpgrade = gameUpgrades.speedUpgrades[0];

        Debug.Log(nextSwordUpgrade.description + " , " + nextSwordUpgrade.cost + " , " + nextSwordUpgrade.value);
        Debug.Log(nextHealthUpgrade.description + " , " + nextHealthUpgrade.cost + " , " + nextHealthUpgrade.value);
        Debug.Log(nextSpeedUpgrade.description + " , " + nextSpeedUpgrade.cost + " , " + nextSpeedUpgrade.value);

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

    public Upgrade ChangeTier(Upgrades listUpgrades, Upgrade nextUpgrade) 
    {
        int currentTier = nextUpgrade.tier;

        switch (nextUpgrade.type) 
        {
            case typeUpgrade.Sword:
                if (currentTier < listUpgrades.swordUpgrades.Count) 
                {
                    currentSwordUpgrade = listUpgrades.swordUpgrades[currentTier];
                    nextUpgrade = listUpgrades.swordUpgrades[currentTier++];                    
                }
                
                break; 
            case typeUpgrade.Health:
                if (currentTier < listUpgrades.healthUpgrades.Count)
                {
                    currentHealthUpgrade = listUpgrades.healthUpgrades[currentTier];
                    nextUpgrade = listUpgrades.healthUpgrades[currentTier++];
                }
                
                break; 
            case typeUpgrade.Speed: 
                {
                    if (currentTier < listUpgrades.speedUpgrades.Count)
                    {
                        currentSpeedUpgrade = listUpgrades.speedUpgrades[currentTier];
                        nextUpgrade = listUpgrades.speedUpgrades[currentTier++];
                    }
                }
                
                break;

        }

        return nextUpgrade; 
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

    public void UpdatePlayerCurrency(int num) 
    {
        playerCurrency = num;
    }


       

}
