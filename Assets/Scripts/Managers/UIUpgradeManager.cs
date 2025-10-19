using UnityEngine;
using TMPro;
public class UIUpgradeManager : MonoBehaviour
{

    [Header("Sword Upgrade References")]
    public TextMeshProUGUI swordUpgradeName;
    public TextMeshProUGUI swordUpgradeDescription;
    public TextMeshProUGUI swordUpgradeCost;
    private int currentSwordTier;

    [Header("Health Upgrade Reference")]
    public TextMeshProUGUI healthUpgradeName;
    public TextMeshProUGUI healthUpgradeDescription;
    public TextMeshProUGUI healthUpgradeCost;
    private int currentHealthTier;

    [Header("Speed Upgrade Reference")]
    public TextMeshProUGUI speedUpgradeName;
    public TextMeshProUGUI speedUpgradeDescription;
    public TextMeshProUGUI speedUpgradeCost;
    private int currentSpeedTier;

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
        
        if(currentSwordTier != upgradeManager.currentSwordUpgrade.tier || currentHealthTier != upgradeManager.currentHealthUpgrade.tier ||
            currentSpeedTier != upgradeManager.currentSpeedUpgrade.tier) 
        {
            UpdateUpgradeUI();
            SettingTiers(); 
        }


    }

    public void UpdateUpgradeUI() 
    {
        // For sword Upgrade

        swordUpgradeName.text = upgradeManager.currentSwordUpgrade.name;
        swordUpgradeDescription.text = upgradeManager.currentSwordUpgrade.description;

        if (upgradeManager.currentSwordUpgrade.tier < 5)
        {
            swordUpgradeCost.text = "Cost: " + (upgradeManager.currentSwordUpgrade.cost).ToString();
        }
        else
            swordUpgradeCost.text = " - "; 



            // For health upgrade

        healthUpgradeName.text = upgradeManager.currentHealthUpgrade.name;
        healthUpgradeDescription.text = upgradeManager.currentHealthUpgrade.description;

        if (upgradeManager.currentHealthUpgrade.tier < 5)
        {
            healthUpgradeCost.text = "Cost: " + (upgradeManager.currentHealthUpgrade.cost).ToString();
        }
        else
            healthUpgradeCost.text = " - ";

            // For speed upgrade

        speedUpgradeName.text = upgradeManager.currentSpeedUpgrade.name;
        speedUpgradeDescription.text = upgradeManager.currentSpeedUpgrade.description;

        if (upgradeManager.currentSpeedUpgrade.tier < 5)
        {
            speedUpgradeCost.text = "Cost: " + (upgradeManager.currentSpeedUpgrade).cost.ToString();
        }
        else
            speedUpgradeCost.text = " - ";

    }

    public void SettingTiers() 
    {
        currentSwordTier = upgradeManager.currentSwordUpgrade.tier;
        currentHealthTier = upgradeManager.currentHealthUpgrade.tier;
        currentSpeedTier = upgradeManager.currentSpeedUpgrade.tier;
    }


}
