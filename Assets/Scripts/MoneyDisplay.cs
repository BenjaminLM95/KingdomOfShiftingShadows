using UnityEngine;
using TMPro; 

public class MoneyDisplay : MonoBehaviour
{
    private int playerMoney; 
    private UpgradeManager upgradeManager;
    public TextMeshProUGUI moneyText; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        playerMoney = upgradeManager.playerCurrency;
        moneyText.text = "Money: " + playerMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMoney != upgradeManager.playerCurrency) 
        {
            playerMoney = upgradeManager.playerCurrency;
            moneyText.text = "Money: " + playerMoney.ToString(); 
        }
    }
}
