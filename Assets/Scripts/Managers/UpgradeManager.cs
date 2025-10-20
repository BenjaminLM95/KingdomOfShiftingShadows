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

    public int index = 0; 

    public int playerCurrency {  get; private set; }     

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameUpgrades = new Upgrades();
       

        SetAllUpgrades(); 

        nextSwordUpgrade = gameUpgrades.swordUpgrades[index]; 
        nextHealthUpgrade = gameUpgrades.healthUpgrades[index];
        nextSpeedUpgrade = gameUpgrades.speedUpgrades[index];

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

    public Upgrade ChangeTier(Upgrades listUpgrades, Upgrade _upgrade) 
    {        

        switch (_upgrade.type) 
        {
            case typeUpgrade.Sword:
                if (index < listUpgrades.swordUpgrades.Count) 
                {                    
                    currentSwordUpgrade = listUpgrades.swordUpgrades[index];
                    index++;
                    _upgrade = listUpgrades.swordUpgrades[index];                    
                }
                
                break; 
            case typeUpgrade.Health:
                if (index < listUpgrades.healthUpgrades.Count)
                {
                    currentHealthUpgrade = listUpgrades.healthUpgrades[index];
                    index++;
                    _upgrade = listUpgrades.healthUpgrades[index];
                }
                
                break; 
            case typeUpgrade.Speed: 
                {
                    if (index < listUpgrades.speedUpgrades.Count)
                    {
                        currentSpeedUpgrade = listUpgrades.speedUpgrades[index];
                        index++;
                        _upgrade = listUpgrades.speedUpgrades[index];
                    }
                }
                
                break;

        }

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

    public void UpdatePlayerCurrency(int num) 
    {
        playerCurrency = num;
    }


       

}
