using UnityEngine;
using TMPro; 


public enum EnemyState 
{
    Running,
    Agressive,
    Collapse,
    None
}

public enum EnemyType 
{
    DayEnemy,
    NightEnemy
}

public class Enemy : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();

    [Header("Base Stats")]
    [SerializeField] private int baseMaxHP;    
    [SerializeField] private int baseAtk;
    [SerializeField] private float baseSpeed; 


    [Header("Current Stats")]
    [SerializeField] private int maxHP;
    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private float speed = 0;
    [SerializeField] private Vector2 velocity;
    public bool alive = true;

    public bool isWalking = true; 

    [Header("Reference")]
    [SerializeField] GameObject _player; 
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject kingdomsGate;
    public TextMeshPro hpText;
    [SerializeField] DayNightManager dayNightManager;
    public Rigidbody2D rb2;


    [Header("Behavior")]
    public EnemyState enemyState;
    [SerializeField] private Animator enemyAnimator; 
    public EnemyType enemyType;

    [Header("Other variables")]
    public bool invincibility = false;
    public float agressiveStateCooldown = 4f;
    public float stateCount = 0f;
    public int moneyValue = 0;
    public bool isDefeat = false;
    public bool isHit = false;
    [SerializeField] private SoundsManager soundManager;



    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        kingdomsGate = GameObject.Find("KingdomGate");
        enemyAnimator = GetComponent<Animator>();
        dayNightManager = FindFirstObjectByType<DayNightManager>();
        soundManager = FindFirstObjectByType<SoundsManager>();
        rb2 = GetComponent<Rigidbody2D>(); 

        int nDay = dayNightManager.dayCount;
        isWalking = true; 

        if (enemyType == EnemyType.DayEnemy)
        {
            SetBaseStats(1 + nDay * 5, 2 + nDay, 1.5f + (nDay / 2), 10 * nDay);
        }
        else if (enemyType == EnemyType.NightEnemy)
        {
            SetBaseStats(nDay * 4 - 1 , 2 + nDay, 0.8f + (nDay / 4), 0);
        }

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        

        enemyState = EnemyState.Running;   //The enemy start running to the kingdom gates direction
        velocity = new Vector2(-1, 0);
                

        SetStats();

        UpdateHPText();

        if (enemyType == EnemyType.DayEnemy) 
        {
            //soundManager.PlaySound("WitchLaugh"); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAction();

        if (!alive) 
        {
            if (enemyType == EnemyType.NightEnemy)
            {                
                enemyAnimator.SetBool("isFaded", true);                 
                Invoke("SetInactive", 3f); 
            }
        }
    }

    private void LateUpdate()
    {
        if (hp != healthSystem.health)
        {
            //Debug.Log(healthSystem.health); 
            hp = healthSystem.health;
            UpdateHPText();

        }

        if (healthSystem.health <= 0 && !isDefeat)
        {
            if (enemyType == EnemyType.DayEnemy)
            {
                isDefeat = true;
                enemyAnimator.SetBool("isFainted", true);
                soundManager.PlaySound("WitchDead"); 
                playerController.numKill++;
                playerController.upgradeManager.ObtainingMoneyReward(moneyValue);
                Invoke("DeathBehavior", 1f);
            }
            else 
            {
                soundManager.PlaySound("ZombieDefeated"); 
                DeathBehavior(); 
            }
        }
    }

    public void SetBaseStats(int bMaxHp, int bAtk, float bSpeed, int bMoneyValue) 
    {
        baseMaxHP = bMaxHp;      
        baseAtk = bAtk;
        baseSpeed = bSpeed;
        moneyValue = bMoneyValue; 

    }


    public void SetStats() 
    {
        maxHP = baseMaxHP;
        hp = maxHP;
        atk = baseAtk;
        speed = baseSpeed;        


        healthSystem.setMaxHP(maxHP);
        hp = healthSystem.maxHealth; 
        healthSystem.health = healthSystem.maxHealth; 
        healthSystem.setAttack(atk); 

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Sword") && !invincibility) 
        {
            isHit = true;
            rb2.bodyType = RigidbodyType2D.Dynamic;
            isWalking = false;
            if (enemyType == EnemyType.DayEnemy)
            {
                enemyAnimator.SetBool("isWalking", false);
            }
             
            //Debug.Log("Collision with sword");
            healthSystem.TakeDamage(playerController.swordPower);
            Debug.Log(healthSystem.health);
            invincibility = true;
            enemyAnimator.SetBool("isDamaged", true);
            rb2.linearVelocity = Vector2.zero;
            rb2.AddForceX(1f, ForceMode2D.Impulse);
            SetAgressiveMode();
            Invoke("vulnerability", 1f);
        }

       
    }       

    private void UpdateHPText() 
    {
        hpText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth;
    }

    private void vulnerability() 
    {
        invincibility = false;
        enemyAnimator.SetBool("isDamaged", false);
        isHit = false;
        isWalking = true;
        rb2.bodyType = RigidbodyType2D.Kinematic;
        if (enemyType == EnemyType.DayEnemy)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
    }

    private void EnemyMove() 
    {
        if(!isHit)       
            rb2.MovePosition(rb2.position + velocity * speed * Time.fixedDeltaTime);
            //transform.position += new Vector3(velocity.x, velocity.y, 0f) * speed * Time.deltaTime; 
    }

    private void EnemyAction() 
    {
        switch (enemyState) 
        {
            case EnemyState.Running:
                RunningThroughKingdomGates();                
                break;
            case EnemyState.Agressive:
                AttackingPlayer();                
                stateCount += Time.deltaTime;               
                SetRunningMode();
                break;
            case EnemyState.Collapse:
                invincibility = true;                
                speed = 0f;
                rb2.linearVelocity = Vector2.zero;
                //rb2.linearVelocity = new Vector2(0,0); 
                break; 
            default:
                RunningThroughKingdomGates();
                break; 
        }

    }

    private void SetAgressiveMode() 
    {
        if (enemyState != EnemyState.Agressive)
        {
            enemyState = EnemyState.Agressive;

            stateCount = 0;
             
        }
    }

    private void SetRunningMode() 
    {
        if(stateCount > agressiveStateCooldown) 
        {
            enemyState = EnemyState.Running;
            stateCount = 0;
            //Debug.Log("To Running"); 
            //Debug.Log("Time: " + stateCount + " , " + Time.time); 
        }
    }


    private void RunningThroughKingdomGates() 
    {
        if (isWalking) 
        {              
            if (enemyType == EnemyType.DayEnemy)
            {
                enemyAnimator.SetBool("isWalking", true);
            }
        }            

        velocity = (kingdomsGate.transform.position - transform.position).normalized;
        EnemyMove(); 
    }
    
    private void AttackingPlayer() 
    {
        if (_player == null)
            _player = GameObject.Find("Player");

        if (_player != null)
        {
            float distanceWithPlayer = Vector2.Distance(transform.position, _player.transform.position);

            if (distanceWithPlayer > 0f)
            {
                velocity = (_player.transform.position - transform.position).normalized;
                EnemyMove();
            }
            else
            {
                //Debug.Log("Attack"); 
            }
        }
    }

    private void DeathBehavior() 
    {
        switch (enemyType) 
        {
            case EnemyType.DayEnemy:                                
                isDefeat = true; 
                this.gameObject.SetActive(false);
                break; 
            case EnemyType.NightEnemy:
                Debug.Log("Colapse"); 
                enemyState = EnemyState.Collapse;
                isDefeat = true;
                Invoke("ReviveEnemy", 5f);                
                break;
            default:
                this.gameObject.SetActive(false);
                break;

        }
    }

    public void ReviveEnemy() 
    {
        SetStats();
        invincibility = false;
        isDefeat = false; 
        enemyState = EnemyState.Running;
        UpdateHPText();
    }


    private void SetInactive() 
    {
        gameObject.SetActive(false);
    }
}
