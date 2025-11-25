using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public HealthSystem healthSystem = new HealthSystem();
    [SerializeField] private int playerHealth;
    [SerializeField] private bool invincibility = false;
    [SerializeField] private float vulnerabilityCooldown = 0.5f; 
    public TextMeshProUGUI healthText;
    public GameObject healthSlider;
    Slider hpSlider;
    public GameObject shieldSlider;
    Slider shieldBar;
    [SerializeField] private int playerShield; 
    



    [Header("Reference")]
    [SerializeField] private Enemy attackingEnemy = null;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private Animator playerHealthAnimator;
    [SerializeField] private CameraShaking cameraShaking;
    public PlayerController _playerController; 
    public int upgradeHealhValue;
    private Coroutine myCoroutineReference;

    

   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettingInitialStats(); 
        upgradeManager = FindFirstObjectByType<UpgradeManager>();         
        playerHealthAnimator = GetComponent<Animator>();
        // Setting the HP bar
        healthSlider = GameObject.Find("PlayerHPSlider");
        hpSlider = healthSlider.GetComponent<Slider>();        
        hpSlider.value = (float)healthSystem.health / (float)healthSystem.maxHealth;
        ChangingHealthBarColor(hpSlider.value);
        // Setting the shield Bar
        shieldSlider = GameObject.Find("ShieldHPSlider");
        shieldBar = shieldSlider.GetComponent<Slider>();
        shieldBar.value = (float)healthSystem.shield / (float)healthSystem.maxShield;
        SetShieldBarColor(); 


    }

    // Update is called once per frame
    void Update()
    {
        

        if(playerHealth != healthSystem.health) 
        {                     
            healthText.text = "Player's HP: " + healthSystem.health + " / " + healthSystem.maxHealth;
            playerHealth = healthSystem.health;
            hpSlider.value = (float)healthSystem.health / (float)healthSystem.maxHealth;
            ChangingHealthBarColor(hpSlider.value);

        }       

        if (healthSystem.health <= 0) 
        {
            _playerController.PlayerDeath(); 
            
        }
               
        if(playerShield != healthSystem.shield) 
        {
            shieldBar.value = (float)healthSystem.shield / (float)healthSystem.maxShield;
            playerShield = healthSystem.shield; 

            if(playerShield <= 0) 
            {
                shieldBar.fillRect.gameObject.SetActive(false); 
            }
            else 
            {
                shieldBar.fillRect.gameObject.SetActive(true);
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

    private void SettingShield() 
    {
        healthSystem.setMaxShield(playerShield);
        healthSystem.shield = healthSystem.maxShield; 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ReceiveDamageByEnemy(col);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        ReceiveDamageByEnemy(collision);
    }

    private void ReceiveDamageByEnemy(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Enemy"))
        {

            if (attackingEnemy == null && !invincibility)
            {

                attackingEnemy = col.gameObject.GetComponent<Enemy>();
                if ((attackingEnemy.alive || attackingEnemy.enemyType == EnemyType.DayEnemy) && attackingEnemy.enemyState != EnemyState.Collapse
                    && !attackingEnemy.isDefeat)
                {
                    attackingEnemy.SetAttackAnimation();
                    healthSystem.TakeDamage(attackingEnemy.healthSystem.baseAttack);
                    _playerController._canAttack = false;
                    _playerController.soundManager.PlaySoundFXClip("PlayerHurt");

                    if (cameraShaking == null)
                    {
                        cameraShaking = FindFirstObjectByType<CameraShaking>();
                    }

                    if (cameraShaking != null)
                    {
                        myCoroutineReference = StartCoroutine(cameraShaking.Shake2(0.25f, 0.125f));
                    }

                    if (attackingEnemy.enemyType == EnemyType.DayEnemy && healthSystem.health <= 0 && _playerController.playerState != PlayerState.Death)
                    {
                        _playerController.soundManager.PlaySoundFXClip("WitchLaugh");
                        invincibility = false; 
                        Debug.Log(invincibility);
                        return; 
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
        _playerController._canAttack = true; 
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
        playerShield = 10; 
        invincibility = false;
        SettingHealth();
        SettingShield(); 
        healthText.text = "Player's HP: " + healthSystem.health + " / " + healthSystem.maxHealth;
    }

    private void ChangingHealthBarColor(float _value) 
    {
        Image fillImage = hpSlider.fillRect.GetComponent<Image>();

        if (_value > 0.5) 
        {
            fillImage.color = Color.green; 
        }
        else if(_value > 0.25) 
        {
            fillImage.color = Color.yellow; 
        }
        else if(_value <= 0.25) 
        {
            fillImage.color = Color.red;
        }
    }

    private void SetShieldBarColor() 
    {
        Image fillImage = shieldBar.fillRect.GetComponent<Image>();
        Color orangeColor = new Color32(255, 165, 0, 255);

        fillImage.color = orangeColor;
    }

    private void OnDisable()
    {
        StopAllCoroutines(); 
    }


    
}
