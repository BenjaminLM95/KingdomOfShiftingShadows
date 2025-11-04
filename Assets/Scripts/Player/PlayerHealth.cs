using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public HealthSystem healthSystem = new HealthSystem();
    [SerializeField] private int playerHealth;
    [SerializeField] private bool invincibility = false;
    [SerializeField] private float vulnerabilityCooldown = 0.5f; 
    public TextMeshProUGUI healthText;



    [Header("Reference")]
    [SerializeField] private Enemy attackingEnemy = null;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private Animator playerHealthAnimator;
    [SerializeField] private CameraShaking cameraShaking;
    public PlayerController _playerController; 
    private int upgradeHealhValue;
    private Coroutine myCoroutineReference;

    

   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettingInitialStats(); 
        upgradeManager = FindFirstObjectByType<UpgradeManager>();         
        playerHealthAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(playerHealth != healthSystem.health) 
        {                     
            healthText.text = "Player's HP: " + healthSystem.health + " / " + healthSystem.maxHealth;
            playerHealth = healthSystem.health;    

        }

        if (upgradeManager.isHealthUpgrade && upgradeManager.currentHealthUpgrade != null)
        {
            if (upgradeHealhValue != upgradeManager.currentHealthUpgrade.value)
            {
                upgradeHealhValue = upgradeManager.currentHealthUpgrade.value;          
                UpdatingHealthUpgrade(); 
            }
        }
        

    }

    public void StopMyCoroutine() 
    {
        if(myCoroutineReference != null) 
        {
            StopCoroutine(myCoroutineReference);
            myCoroutineReference = null; 
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
                if ((attackingEnemy.alive || attackingEnemy.enemyType == EnemyType.DayEnemy) && attackingEnemy.enemyState != EnemyState.Collapse 
                    &&  !attackingEnemy.isDefeat)
                {                   
                    attackingEnemy.SetAttackAnimation();                   
                    healthSystem.TakeDamage(attackingEnemy.healthSystem.baseAttack);

                    if (cameraShaking == null)
                    {
                        cameraShaking = FindFirstObjectByType<CameraShaking>();
                    }

                    if (cameraShaking != null)
                    {
                        myCoroutineReference = StartCoroutine(cameraShaking.Shake2(0.25f, 0.125f));
                    }                    

                    if (attackingEnemy.enemyType == EnemyType.DayEnemy && healthSystem.health <= 0) 
                    {
                       _playerController.soundManager.PlaySoundFXClip("WitchLaugh", transform);
                    }

                    playerHealthAnimator.SetBool("isDamaged", true);
                    invincibility = true;
                    attackingEnemy = null;
                    Invoke("vulnerability", vulnerabilityCooldown);
                }

                if (attackingEnemy != null)
                    attackingEnemy = null; 
            }
            
        }
    }

    public void vulnerability() 
    {
        invincibility = false;
        playerHealthAnimator.SetBool("isDamaged", false);
    }


    public void UpdatingHealthUpgrade() 
    {
        healthSystem.setMaxHP(15 + upgradeHealhValue);
        healthSystem.health = healthSystem.maxHealth;
        Debug.Log("Health upgraded  " + healthSystem.maxHealth); 
    }

    public void SettingInitialStats() 
    {
        upgradeHealhValue = 0; 
        playerHealth = 15;
        invincibility = false;
        SettingHealth();
        healthText.text = "Player's HP: " + healthSystem.health + " / " + healthSystem.maxHealth;
    }

    private void OnDisable()
    {
        StopAllCoroutines(); 
    }


    
}
