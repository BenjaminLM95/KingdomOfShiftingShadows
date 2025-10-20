using UnityEngine;
using TMPro; 

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public HealthSystem healthSystem = new HealthSystem();
    [SerializeField] private int playerHealth;
    [SerializeField] private bool invincibility = false;
    [SerializeField] private float vulnerabilityCooldown = 1.5f; 
    public TextMeshProUGUI healthText;



    [Header("Reference")]
    [SerializeField] private Enemy attackingEnemy = null;
    [SerializeField] private UpgradeManager upgradeManager;
    private int upgradeHealhValue; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = 15;
        SettingHealth();
        healthText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth;
        upgradeManager = FindFirstObjectByType<UpgradeManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth != healthSystem.health) 
        {
            healthText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth;
            playerHealth = healthSystem.health;
        }

        if (upgradeManager.isHealthUpgrade && upgradeManager.currentHealthUpgrade != null)
        {
            if (upgradeHealhValue != upgradeManager.currentHealthUpgrade.value)
            {
                upgradeHealhValue = upgradeManager.currentHealthUpgrade.value;
                Debug.Log(upgradeHealhValue);
                UpdatingHealthUpgrade(); 
            }
        }


    }

    private void SettingHealth() 
    {
        healthSystem.setMaxHP(playerHealth);
        healthSystem.health = healthSystem.maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy")) 
        {
            if (attackingEnemy == null && !invincibility) 
            { 
                attackingEnemy = col.gameObject.GetComponent<Enemy>();
                healthSystem.TakeDamage(attackingEnemy.healthSystem.baseAttack);
                invincibility = true;
                attackingEnemy = null; 
                Invoke("vulnerability", vulnerabilityCooldown); 
            }
            
        }
    }

    private void vulnerability() 
    {
        invincibility = false; 
    }


    public void UpdatingHealthUpgrade() 
    {
        healthSystem.setMaxHP(15 + upgradeHealhValue);
        healthSystem.health = healthSystem.maxHealth;
        Debug.Log("Health upgraded  " + healthSystem.maxHealth); 
    }

}
