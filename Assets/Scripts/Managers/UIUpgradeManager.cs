using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIUpgradeManager : MonoBehaviour
{

    [Header("Sword Upgrade References")]
    public TextMeshProUGUI swordUpgradeName;
    public TextMeshProUGUI swordUpgradeDescription;
    public TextMeshProUGUI swordUpgradeCost;
    public TextMeshProUGUI swordUpgradeLevel; 
    [SerializeField] private int currentSwordTier;


    [Header("Health Upgrade Reference")]
    public TextMeshProUGUI healthUpgradeName;
    public TextMeshProUGUI healthUpgradeDescription;
    public TextMeshProUGUI healthUpgradeCost;
    public TextMeshProUGUI healthUpgradeLevel; 
    [SerializeField] private int currentHealthTier;

    [Header("Speed Upgrade Reference")]
    public TextMeshProUGUI speedUpgradeName;
    public TextMeshProUGUI speedUpgradeDescription;
    public TextMeshProUGUI speedUpgradeCost;
    public TextMeshProUGUI speedUpgradeLevel; 
    [SerializeField] private int currentSpeedTier;

    public TextMeshProUGUI currencyText;

    public Button swordUpgradeButton;
    public Button healthUpgradeButton;
    public Button speedUpgradeButton; 

    [Header("Upgrade Manager Reference")]
    [SerializeField] private UpgradeManager upgradeManager;

    private void Start()
    {    
        upgradeManager = FindFirstObjectByType<UpgradeManager>();

        SettingTiers();
        UpdateUpgradeUI();

    }

    private void Update()
    {        
        
        if(currentSwordTier != upgradeManager.nextSwordUpgrade.tier || currentHealthTier != upgradeManager.nextHealthUpgrade.tier ||
            currentSpeedTier != upgradeManager.nextSpeedUpgrade.tier) 
        {            
            SettingTiers();
            UpdateUpgradeUI();
        }

        currencyText.text = upgradeManager.playerCurrency.ToString() + " $"; 


        if(upgradeManager.nextSwordUpgrade.cost > upgradeManager.playerCurrency) 
        {
            swordUpgradeButton.image.color = Color.gray; 
        }
        else 
        {
            swordUpgradeButton.image.color = Color.white;
        }

        if (upgradeManager.nextHealthUpgrade.cost > upgradeManager.playerCurrency)
        {
            healthUpgradeButton.image.color = Color.gray;
        }
        else
        {
            healthUpgradeButton.image.color = Color.white;
        }

        if (upgradeManager.nextSpeedUpgrade.cost > upgradeManager.playerCurrency)
        {
            speedUpgradeButton.image.color = Color.gray;
        }
        else
        {
            speedUpgradeButton.image.color = Color.white;
        }

    }

    public void UpdateUpgradeUI() 
    {
        // For sword Upgrade

        swordUpgradeName.text = upgradeManager.nextSwordUpgrade.name;
        swordUpgradeDescription.text = upgradeManager.nextSwordUpgrade.description;

        if (upgradeManager.nextSwordUpgrade.tier < 5)
        {
            swordUpgradeCost.text = "Cost: " + (upgradeManager.nextSwordUpgrade.cost).ToString() + " $";
        }
        else
            swordUpgradeCost.text = " - ";

        swordUpgradeLevel.text = (currentSwordTier-1).ToString() + "/4"; 

            // For health upgrade

        healthUpgradeName.text = upgradeManager.nextHealthUpgrade.name;
        healthUpgradeDescription.text = upgradeManager.nextHealthUpgrade.description;

        if (upgradeManager.nextHealthUpgrade.tier < 5)
        {
            healthUpgradeCost.text = "Cost: " + (upgradeManager.nextHealthUpgrade.cost).ToString() + " $";
        }
        else
            healthUpgradeCost.text = " - ";

        healthUpgradeLevel.text = (currentHealthTier-1).ToString() + "/4"; 

            // For speed upgrade

        speedUpgradeName.text = upgradeManager.nextSpeedUpgrade.name;
        speedUpgradeDescription.text = upgradeManager.nextSpeedUpgrade.description;

        if (upgradeManager.nextSpeedUpgrade.tier < 5)
        {
            speedUpgradeCost.text = "Cost: " + (upgradeManager.nextSpeedUpgrade).cost.ToString() + " $";
        }
        else
            speedUpgradeCost.text = " - ";

        speedUpgradeLevel.text = (currentSpeedTier-1).ToString() + "/4";
    }

    public void SettingTiers() 
    {
        currentSwordTier = upgradeManager.nextSwordUpgrade.tier;
        currentHealthTier = upgradeManager.nextHealthUpgrade.tier;
        currentSpeedTier = upgradeManager.nextSpeedUpgrade.tier;
    }

    public void ResetUpgradeUI() 
    {
        SettingTiers();
        UpdateUpgradeUI();
    }


}
