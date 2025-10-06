using UnityEngine;
using TMPro; 


public enum EnemyState 
{
    Running,
    Agressive,
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
    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private float speed = 1.5f;
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

        hp = 4;
        atk = 2; 
        SetStats();

        hpText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth; 
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
            hpText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth;

        }

        if (healthSystem.health <= 0)
        {
            playerController.getOneKill();
            Debug.Log(playerController.numKill);
            DeathBehavior();
        }
    }

    public void SetStats() 
    {
        healthSystem.setMaxHP(hp);
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
            //Debug.Log(healthSystem.health);
            invincibility = true;
            enemyAnimator.SetBool("isDamaged", true);
            SetAgressiveMode();           
            Invoke("vulnerability", 1f);
        }

       
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
                //Debug.Log(stateCount);
                SetRunningMode();
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
            //Debug.Log("Start chasing player"); 
            //Debug.Log("Time: " + Time.time + "StateCount" + stateCount); 
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
                this.gameObject.SetActive(false);
                break; 
            case EnemyType.NightEnemy:
                invincibility = true;
                speed = 0f;
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
        speed = 1.5f;
        this.gameObject.SetActive(true); 
    }

}
