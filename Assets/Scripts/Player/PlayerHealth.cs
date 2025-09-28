using UnityEngine;
using TMPro; 

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public HealthSystem healthSystem = new HealthSystem();
    [SerializeField] private int playerHealth;
    [SerializeField] private bool invincibility = false;
    [SerializeField] private float vulnerabilityCooldown = 1.5f; 
    public TextMeshPro healthText;



    [Header("Reference")]
    [SerializeField] private Enemy attackingEnemy = null; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = 15;
        SettingHealth();
        healthText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth != healthSystem.health) 
        {
            healthText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth;
            playerHealth = healthSystem.health;
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

}
