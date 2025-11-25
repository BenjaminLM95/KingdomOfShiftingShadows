using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIUpgradeManager : MonoBehaviour
{

    [Header("Sword Upgrade References")]
    public TextMeshProUGUI swordUpgradeDescription;
    public TextMeshProUGUI swordUpgradeCost;
    
    [SerializeField] private int currentSwordTier;
    public GameObject swordTier1;
    public GameObject swordTier2;
    public GameObject swordTier3;
    public GameObject swordTier4;


    [Header("Health Upgrade Reference")]
    public TextMeshProUGUI healthUpgradeDescription;
    public TextMeshProUGUI healthUpgradeCost;    
    [SerializeField] private int currentHealthTier;
    public GameObject healthTier1; 
    public GameObject healthTier2;
    public GameObject healthTier3;
    public GameObject healthTier4;

    [Header("Speed Upgrade Reference")]
    public TextMeshProUGUI speedUpgradeDescription;
    public TextMeshProUGUI speedUpgradeCost;    
    [SerializeField] private int currentSpeedTier;
    public GameObject speedTier1;
    public GameObject speedTier2;
    public GameObject speedTier3;
    public GameObject speedTier4;

    [Header("KnockBack Upgrade Reference")]
    public TextMeshProUGUI knockbackUpgradeDescription;
    public TextMeshProUGUI knockbackUpgradeCost;    
    [SerializeField] private int currentKnockbackTier;
    public GameObject knockbackTier1;
    public GameObject knockbackTier2;
    public GameObject knockbackTier3;
    public GameObject knockbackTier4;

    [Header("Ice Magic Upgrade Reference")]
    public TextMeshProUGUI iceMagicDescription;
    public TextMeshProUGUI iceMagicCost;

    [Header("Wind Slash Upgrade Reference")]
    public TextMeshProUGUI windSlashDescription;
    public TextMeshProUGUI windSlashCost; 

    public TextMeshProUGUI currencyText;

    public Button swordUpgradeButton;
    public Button healthUpgradeButton;
    public Button speedUpgradeButton;
    public Button knockbackUpgradeButton; 
    public Button iceMagicButton;
    public Button windSlashButton;

    [Header("Upgrade Manager Reference")]
    [SerializeField] private UpgradeManager upgradeManager;

    private FreezeMagic fmRef = new FreezeMagic();
    private WindSlash wsRef = new WindSlash();
     

    private void Start()
    {    
        upgradeManager = FindFirstObjectByType<UpgradeManager>();

        SettingTiers();
        UpdateUpgradeUI();

    }

    private void Update()
    {        
        
        if(currentSwordTier != upgradeManager.nextSwordUpgrade.tier || currentHealthTier != upgradeManager.nextHealthUpgrade.tier ||
            currentSpeedTier != upgradeManager.nextSpeedUpgrade.tier || currentKnockbackTier != upgradeManager.nextKnockbackUpgrade.tier) 
        {            
            SettingTiers();
            UpdateUpgradeUI();
        }

        currencyText.text = upgradeManager.playerCurrency.ToString(); 


        if(upgradeManager.nextSwordUpgrade.cost > upgradeManager.playerCurrency || upgradeManager.currentSwordUpgrade.tier >= 5) 
        {
            swordUpgradeButton.interactable = false; 
        }
        else 
        {            
            swordUpgradeButton.interactable = true;
        }

        if (upgradeManager.nextHealthUpgrade.cost > upgradeManager.playerCurrency || upgradeManager.currentHealthUpgrade.tier >= 5)
        {            
            healthUpgradeButton.interactable = false; 
        }
        else
        {            
            healthUpgradeButton.interactable = true; 
        }

        if (upgradeManager.nextSpeedUpgrade.cost > upgradeManager.playerCurrency || upgradeManager.currentSpeedUpgrade.tier >= 5)
        {            
            speedUpgradeButton.interactable = false;
        }
        else
        {          
            speedUpgradeButton.interactable = true; 
        }

        if(upgradeManager.nextKnockbackUpgrade.cost > upgradeManager.playerCurrency || upgradeManager.currentKnockbackUpgrade.tier >= 5) 
        {
            knockbackUpgradeButton.interactable = false;
        }
        else 
        {            
            knockbackUpgradeButton.interactable = true; 
        }

        if(upgradeManager.playerCurrency < fmRef.itemCost || UpgradeManager.playerInventory.inventory.Count > 2) 
        {
            iceMagicButton.interactable = false;
        }
        else 
        {
            iceMagicButton.interactable = true;
        }

        if(upgradeManager.playerCurrency < wsRef.itemCost || UpgradeManager.playerInventory.inventory.Count > 2) 
        {
            windSlashButton.interactable = false;
        }
        else 
        {
            windSlashButton.interactable = true;
        }

    }

    public void UpdateUpgradeUI() 
    {
        // For sword Upgrade        
        swordUpgradeDescription.text = upgradeManager.nextSwordUpgrade.description;

        if (upgradeManager.nextSwordUpgrade.tier < 5)
        {
            swordUpgradeCost.text = "Cost: " + " $:" + (upgradeManager.nextSwordUpgrade.cost).ToString();
        }
        else
            swordUpgradeCost.text = " - ";        

            // For health upgrade        
        healthUpgradeDescription.text = upgradeManager.nextHealthUpgrade.description;

        if (upgradeManager.nextHealthUpgrade.tier < 5)
        {
            healthUpgradeCost.text = "Cost: " + "$ " + (upgradeManager.nextHealthUpgrade.cost).ToString();
        }
        else
            healthUpgradeCost.text = " - ";        

            // For speed upgrade        
        speedUpgradeDescription.text = upgradeManager.nextSpeedUpgrade.description;

        if (upgradeManager.nextSpeedUpgrade.tier < 5)
        {
            speedUpgradeCost.text = "Cost: " + "$ " + (upgradeManager.nextSpeedUpgrade.cost).ToString();
        }
        else
            speedUpgradeCost.text = " - ";        

        // For Knockback Upgrade
        knockbackUpgradeDescription.text = upgradeManager.nextKnockbackUpgrade.description;

        if (upgradeManager.nextKnockbackUpgrade.tier < 5)
        {
            knockbackUpgradeCost.text = "Cost: " + "$ " + (upgradeManager.nextKnockbackUpgrade.cost).ToString();
        }
        else
            knockbackUpgradeCost.text = " - ";

        FillingItemInfo(); 

        DisactivateAllTiers();
        ActivateSwordTiers(currentSwordTier - 1);
        ActivateHealthTier(currentHealthTier - 1);
        ActivateSpeedTier(currentSpeedTier - 1);
        ActivatePushTier(currentKnockbackTier - 1);
    }

    public void SettingTiers() 
    {
        currentSwordTier = upgradeManager.nextSwordUpgrade.tier;
        currentHealthTier = upgradeManager.nextHealthUpgrade.tier;
        currentSpeedTier = upgradeManager.nextSpeedUpgrade.tier;
        currentKnockbackTier = upgradeManager.nextKnockbackUpgrade.tier;
    }

    public void ResetUpgradeUI() 
    {
        SettingTiers();
        UpdateUpgradeUI();
    }

    public void FillingItemInfo() 
    {
        FreezeMagic iceMagicRef = new FreezeMagic();
        WindSlash windSlashRef = new WindSlash();

        windSlashDescription.text = "Unleash a Range Wind attack";
        windSlashCost.text = "Cost: $ " + windSlashRef.itemCost;

        iceMagicDescription.text = "Freeze all the enemies";
        iceMagicCost.text = "Cost: $ " + iceMagicRef.itemCost; 
    }


    private void DisactivateAllSwordTiers() 
    {
        swordTier1.SetActive(false); 
        swordTier2.SetActive(false);
        swordTier3.SetActive(false);
        swordTier4.SetActive(false);
    }

    private void DisactivateAllHealthTiers() 
    {
        healthTier1.SetActive(false); 
        healthTier2.SetActive(false);
        healthTier3.SetActive(false); 
        healthTier4.SetActive(false);
    }

    private void DisactivateAllSpeedTiers() 
    {
        speedTier1.SetActive(false);
        speedTier2.SetActive(false);
        speedTier3.SetActive(false);
        speedTier4.SetActive(false);
    }

    private void DisactivateAllKnockbackTiers() 
    {
        knockbackTier1.SetActive(false);
        knockbackTier2.SetActive(false);
        knockbackTier3.SetActive(false);
        knockbackTier4.SetActive(false);
    }

    private void DisactivateAllTiers() 
    {
        DisactivateAllSwordTiers();
        DisactivateAllHealthTiers();
        DisactivateAllSpeedTiers();
        DisactivateAllKnockbackTiers(); 
    }

    private void ActivateSwordTiers(int nTier) 
    {
        if (nTier == 1)
        {
            swordTier1.SetActive(true);
        }
        else if (nTier == 2) 
        {
            swordTier1.SetActive(true);
            swordTier2.SetActive(true);
        }
        else if(nTier == 3) 
        {
            swordTier1.SetActive(true);
            swordTier2.SetActive(true);
            swordTier3.SetActive(true);
        }
        else if(nTier == 4) 
        {
            swordTier1.SetActive(true);
            swordTier2.SetActive(true);
            swordTier3.SetActive(true);
            swordTier4.SetActive(true);
        }
    }

    private void ActivateHealthTier(int nTier) 
    {
        if (nTier == 1)
        {
            healthTier1.SetActive(true);
        }
        else if (nTier == 2)
        {
            healthTier1.SetActive(true);
            healthTier2.SetActive(true);
        }
        else if (nTier == 3)
        {
            healthTier1.SetActive(true);
            healthTier2.SetActive(true);
            healthTier3.SetActive(true);
        }
        else if (nTier == 4)
        {
            healthTier1.SetActive(true);
            healthTier2.SetActive(true);
            healthTier3.SetActive(true);
            healthTier4.SetActive(true);
        }
    }

    private void ActivateSpeedTier(int nTier) 
    {
        if (nTier == 1)
        {
            speedTier1.SetActive(true);
        }
        else if (nTier == 2)
        {
            speedTier1.SetActive(true);
            speedTier2.SetActive(true);
        }
        else if (nTier == 3)
        {
            speedTier1.SetActive(true);
            speedTier2.SetActive(true);
            speedTier3.SetActive(true);
        }
        else if (nTier == 4)
        {
            speedTier1.SetActive(true);
            speedTier2.SetActive(true);
            speedTier3.SetActive(true);
            speedTier4.SetActive(true);
        }
    }

    private void ActivatePushTier(int nTier) 
    {
        if (nTier == 1)
        {
            knockbackTier1.SetActive(true);
        }
        else if (nTier == 2)
        {
            knockbackTier1.SetActive(true);
            knockbackTier2.SetActive(true);
        }
        else if (nTier == 3)
        {
            knockbackTier1.SetActive(true);
            knockbackTier2.SetActive(true);
            knockbackTier3.SetActive(true);
        }
        else if (nTier == 4)
        {
            knockbackTier1.SetActive(true);
            knockbackTier2.SetActive(true);
            knockbackTier3.SetActive(true);
            knockbackTier4.SetActive(true);
        }
    }

   

}
