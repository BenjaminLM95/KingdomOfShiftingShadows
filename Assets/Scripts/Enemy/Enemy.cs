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
    [SerializeField] private int baseHP;
    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private float speed = 0;
    [SerializeField] private Vector2 velocity;
    public bool alive = true; 


    [Header("Reference")]
    [SerializeField] GameObject _player; 
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject kingdomsGate;
    public TextMeshPro hpText;
    [SerializeField] EnemyManager enemyManager; 


    [Header("Behavior")]
    [SerializeField] EnemyState enemyState;
    [SerializeField] private Animator enemyAnimator; 
    public EnemyType enemyType;

    [Header("Other variables")]
    public bool invincibility = false;
    public float agressiveStateCooldown = 4f;
    public float stateCount = 0f;
    public int moneyValue = 0;
    public bool isDefeat = false; 
  


    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        kingdomsGate = GameObject.Find("KingdomGate");
        enemyAnimator = GetComponent<Animator>();
        enemyManager = GetComponent<EnemyManager>(); 
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        

        enemyState = EnemyState.Running;   //The enemy start running to the kingdom gates direction
        velocity = new Vector2(-1, 0);
                
        SetStats();

        UpdateHPText(); 
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAction();

        if (!alive) 
        {
            gameObject.SetActive(false);
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
            DeathBehavior();
        }
    }

    public void SetStats() 
    {
        if(enemyType == EnemyType.DayEnemy) 
        {
            baseHP = 4;
            atk = 2;
            speed = 1.5f;
            moneyValue = 10;
        }
        else if(enemyType == EnemyType.NightEnemy) 
        {
            baseHP = 3;
            atk = 2;
            speed = 0.85f;
            moneyValue = 0;
        }


            healthSystem.setMaxHP(baseHP);
        hp = healthSystem.maxHealth; 
        healthSystem.health = healthSystem.maxHealth; 
        healthSystem.setAttack(atk); 

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Sword") && !invincibility) 
        {
            //Debug.Log("Collision with sword");
            healthSystem.TakeDamage(playerController.swordPower);
            Debug.Log(healthSystem.health);
            invincibility = true;
            enemyAnimator.SetBool("isDamaged", true);
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
    }

    private void EnemyMove() 
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0f) * speed * Time.deltaTime; 
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
        velocity = (kingdomsGate.transform.position - transform.position).normalized;
        EnemyMove(); 
    }
    
    private void AttackingPlayer() 
    {
        if (_player == null)
            _player = GameObject.Find("Player");

        float distanceWithPlayer = Vector2.Distance(transform.position, _player.transform.position); 

        if(distanceWithPlayer > 0f) 
        {
            velocity = (_player.transform.position - transform.position).normalized;
            EnemyMove(); 
        }
        else 
        {
            //Debug.Log("Attack"); 
        }
    }

    private void DeathBehavior() 
    {
        switch (enemyType) 
        {
            case EnemyType.DayEnemy:
                playerController.numKill++;
                playerController.currency += moneyValue;
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

}
