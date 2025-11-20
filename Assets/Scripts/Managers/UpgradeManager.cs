using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Upgrades gameUpgrades;
    

    public Upgrade nextSwordUpgrade;
    public Upgrade nextHealthUpgrade;
    public Upgrade nextSpeedUpgrade;
    public Upgrade nextKnockbackUpgrade;

    public Upgrade currentSwordUpgrade = null;
    public Upgrade currentHealthUpgrade = null;
    public Upgrade currentSpeedUpgrade = null;
    public Upgrade currentKnockbackUpgrade = null; 

    public bool isSwordUpgrade;
    public bool isHealthUpgrade;
    public bool isSpeedUpgrade;
    public bool isKnockbackUpgrade; 

    public int swordIndex = 0;
    public int healthIndex = 0;
    public int speedIndex = 0;
    public int knockbackIndex = 0;

    public static Inventory playerInventory = new Inventory();

    public int playerCurrency {  get; private set; }
    private SoundsManager soundManager;
    [SerializeField] private GameObject inventoryDisplay; 
    public ItemDisplayHandler itemDisplayHandler;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager = FindFirstObjectByType<SoundsManager>();        
        gameUpgrades = new Upgrades();

        playerInventory.SetInventorySlots(3);
        //playerInventory.GetAnItem(new FreezeMagic()); 

        SetAllUpgrades(); 

        nextSwordUpgrade = gameUpgrades.swordUpgrades[swordIndex]; 
        nextHealthUpgrade = gameUpgrades.healthUpgrades[healthIndex];
        nextSpeedUpgrade = gameUpgrades.speedUpgrades[speedIndex];
        nextKnockbackUpgrade = gameUpgrades.pushUpgrades[knockbackIndex]; 

        isSwordUpgrade = false;
        isHealthUpgrade = false;
        isSpeedUpgrade = false;
        isKnockbackUpgrade = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(itemDisplayHandler == null) 
        {
            inventoryDisplay = GameObject.Find("UpgradeItemDisplay");
            itemDisplayHandler = inventoryDisplay.GetComponent<ItemDisplayHandler>();
        }

    }

    public void SetAllUpgrades() 
    {
        gameUpgrades.AddSwordUpgrades();
        gameUpgrades.AddHealthUpgrades();
        gameUpgrades.AddSpeedUpgrades();
        gameUpgrades.AddPushUpgrades(); 
    }

    public void DisplayItemsOnScreen() 
    {
        itemDisplayHandler.UpdateItemImages(playerInventory);
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
            case typeUpgrade.Push: 
                {
                    if(knockbackIndex < listUpgrades.pushUpgrades.Count - 1) 
                    {
                        currentKnockbackUpgrade = listUpgrades.pushUpgrades[knockbackIndex];
                        knockbackIndex++;
                        _upgrade = listUpgrades.pushUpgrades[knockbackIndex]; 
                    }
                    break; 
                }


        }
                 
        return _upgrade; 
    }

        

    public void GetSwordUpgrade() 
    {
        if (playerCurrency >= nextSwordUpgrade.cost)
        {
            playerCurrency -= nextSwordUpgrade.cost;
            soundManager.PlaySoundFXClip("BuySound", transform);

            if (!isSwordUpgrade)
                isSwordUpgrade = true;



            nextSwordUpgrade = ChangeTier(gameUpgrades, nextSwordUpgrade);
            //Debug.Log(nextSwordUpgrade.description + " , " + nextSwordUpgrade.cost + " , " + nextSwordUpgrade.value);
        }
    }

    public void GetHealthUpgrade() 
    {
        if (playerCurrency >= nextHealthUpgrade.cost)
        {
            playerCurrency -= nextHealthUpgrade.cost;
            soundManager.PlaySoundFXClip("BuySound", transform);

            if (!isHealthUpgrade)
                isHealthUpgrade = true;


            nextHealthUpgrade = ChangeTier(gameUpgrades, nextHealthUpgrade);            
            //Debug.Log(nextHealthUpgrade.description + " , " + nextHealthUpgrade.cost + " , " + nextHealthUpgrade.value);
        }
    }

    public void GetSpeedUpgrade() 
    {
        if (playerCurrency >= nextSpeedUpgrade.cost)
        {
            playerCurrency -= nextSpeedUpgrade.cost;
            soundManager.PlaySoundFXClip("BuySound", transform);

            if (!isSpeedUpgrade)
                isSpeedUpgrade = true;


            nextSpeedUpgrade = ChangeTier(gameUpgrades, nextSpeedUpgrade);
            //Debug.Log(nextSpeedUpgrade.description + " , " + nextSpeedUpgrade.cost + " , " + nextSpeedUpgrade.value);
        }
    }     
    
    public void GetKnockbackUpgrade() 
    {
        if(playerCurrency >= nextKnockbackUpgrade.cost) 
        {
            playerCurrency -= nextKnockbackUpgrade.cost;
            soundManager.PlaySoundFXClip("BuySound", transform);

            if (!isKnockbackUpgrade)
                isKnockbackUpgrade = true;

            nextKnockbackUpgrade = ChangeTier(gameUpgrades, nextKnockbackUpgrade);

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
        currentKnockbackUpgrade = null;
        swordIndex = 0;
        healthIndex = 0;
        speedIndex = 0;
        knockbackIndex = 0;
        isSwordUpgrade = false;
        isHealthUpgrade = false;
        isSpeedUpgrade = false;
        isKnockbackUpgrade = false; 
        playerCurrency = 0;
        nextSwordUpgrade = gameUpgrades.swordUpgrades[0];
        nextHealthUpgrade = gameUpgrades.healthUpgrades[0];
        nextSpeedUpgrade = gameUpgrades.speedUpgrades[0];
        nextKnockbackUpgrade = gameUpgrades.pushUpgrades[0]; 
    }
       
    public void AddIceMagic() 
    {
        FreezeMagic refItem = new FreezeMagic();
        if (playerCurrency >= refItem.itemCost && playerInventory.inventory.Count < 3)
        {
            playerCurrency -= refItem.itemCost;

            soundManager.PlaySoundFXClip("BuySound", transform);

            playerInventory.GetAnItem(new FreezeMagic());
            Debug.Log("Buy a freeze magic");

            DisplayItemsOnScreen();
        }
    }

    public void AddWindSlash() 
    {
        WindSlash refItem = new WindSlash();

        if (playerCurrency >= refItem.itemCost && playerInventory.inventory.Count < 3)
        {
            playerCurrency -= refItem.itemCost;

            soundManager.PlaySoundFXClip("BuySound", transform);

            playerInventory.GetAnItem(new WindSlash());
            Debug.Log("Buy Wind Slash");

            DisplayItemsOnScreen(); 
        }
    }

}
