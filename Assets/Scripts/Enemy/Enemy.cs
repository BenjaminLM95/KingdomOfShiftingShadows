using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;


public enum EnemyState 
{
    Running,
    Agressive,
    Collapse,
    Faded,
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
    [SerializeField] CameraShaking cameraShaking; 

    public Rigidbody2D rb2;
    public LayerMask enemyLayerMask;


    [Header("Behavior")]
    public EnemyState enemyState;
    [SerializeField] private Animator enemyAnimator; 
    public EnemyType enemyType;
    [SerializeField] private float enemyDistance = 1f; 

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
        cameraShaking = FindFirstObjectByType<CameraShaking>(); 

        int nDay = dayNightManager.dayCount;
        isWalking = true; 

        if (enemyType == EnemyType.DayEnemy)
        {
            SetBaseStats(1 + nDay * 5, 3 + nDay, 1.5f + (nDay / 2), 10 + ((nDay+1) * (nDay+1)) + (4*(nDay+1)));
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
                enemyState = EnemyState.Faded;
                hpText.gameObject.SetActive(false); 
                enemyAnimator.SetBool("isFaded", true);                 
                Invoke("SetInactive", 2f); 
            }
        }
    }

    private void FixedUpdate()
    {
        if (velocity.x < 0)
        {
            if (enemyType == EnemyType.DayEnemy)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if(enemyType == EnemyType.NightEnemy) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }
        else if (velocity.x > 0)
        {
            if(enemyType == EnemyType.DayEnemy) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if(enemyType == EnemyType.NightEnemy) 
            {
                transform.localScale = new Vector3(1, 1, 1);
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
                soundManager.PlaySoundFXClip("WitchDead", transform);                  
                playerController.numKill++;
                playerController.upgradeManager.ObtainingMoneyReward(moneyValue);
                hpText.gameObject.SetActive(false); 
                Invoke("DeathBehavior", 1f);
            }
            else 
            {
                soundManager.PlaySoundFXClip("ZombieDefeated", transform);                 
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
            StartCoroutine(cameraShaking.Shake(0.25f, 0.125f));           
            isHit = true;
            rb2.bodyType = RigidbodyType2D.Dynamic;
            isWalking = false;            
            enemyAnimator.SetBool("isWalking", false);        
            healthSystem.TakeDamage(playerController.swordPower);            
            invincibility = true;
            enemyAnimator.SetBool("isDamaged", true);
            rb2.linearVelocity = Vector2.zero;
            rb2.AddForceX(playerController.knockBackForce * Mathf.Sign(collision.attachedRigidbody.linearVelocityX), ForceMode2D.Impulse);
            //rb2.AddForceX(Mathf.Lerp(0, playerController.knockBackForce, 1/Time.time), ForceMode2D.Impulse);             
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
            case EnemyState.Faded:
                speed = 0f;
                rb2.linearVelocity = Vector2.zero;
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
        }
    }


    private void RunningThroughKingdomGates() 
    {
        if (isWalking) 
        { 
           enemyAnimator.SetBool("isWalking", true);
        }

        if (isAnotherEnemyClose()) 
        {            
            velocity = Vector2.zero;
        }
        else 
        {
            velocity = (kingdomsGate.transform.position - transform.position).normalized;
        }
                    
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
                enemyState = EnemyState.Collapse;
                isDefeat = true;
                hpText.gameObject.SetActive(false);
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
        hpText.gameObject.SetActive(true);
        invincibility = false;
        isDefeat = false; 
        enemyState = EnemyState.Running;
        UpdateHPText();
    }


    private void SetInactive() 
    {
        gameObject.SetActive(false);
    }

    public void SetAttackAnimation() 
    {
        enemyAnimator.SetBool("isAttacking", true);
        Invoke("ReturnToNormalAnimation", 0.25f); 
    }

    public void ReturnToNormalAnimation() 
    {
        enemyAnimator.SetBool("isAttacking", false);
    }

    public bool isAnotherEnemyClose() 
    {
        RaycastHit2D[] enemyClose = Physics2D.RaycastAll(this.transform.position + Vector3.left, Vector2.left, enemyDistance, enemyLayerMask);
        RaycastHit2D[] enemyClose2 = Physics2D.RaycastAll(this.transform.position + Vector3.left, new Vector2(-1f, 1.41f), enemyDistance, enemyLayerMask);
        RaycastHit2D[] enemyClose3 = Physics2D.RaycastAll(this.transform.position + Vector3.left, new Vector2(-1f, -1.41f), enemyDistance, enemyLayerMask);

        if (enemyClose.Length > 0)
        {
            for (int i = 0; i < enemyClose.Length; i++)
            {
                if (enemyClose[i].transform == this.transform)
                {
                    return false;
                }
                else 
                {
                    return true; 
                }
            }
        }

        if(enemyClose2.Length > 0) 
        { 
            for (int i = 0; i < enemyClose2.Length; i++)
            {
                if (enemyClose2[i].transform == this.transform)
                {
                    return false;
                }
                else 
                {
                    return true; 
                }
            }
            

        }

        if (enemyClose3.Length > 0) 
        {
            for (int i = 0; i < enemyClose3.Length; i++)
            {
                if (enemyClose3[i].transform == this.transform)
                {
                    return false;
                }
                else 
                {
                    return true; 
                }
            }
           
        }

        return false; 
    }

    private void OnDrawGizmos()
    {
        // Set gizmo color
        Gizmos.color = Color.red;

        // Raycast direction
        Vector2 direction = Vector2.left;

        // Draw the ray in the Scene view
        Gizmos.DrawRay(transform.position + Vector3.left, direction * enemyDistance);
        Gizmos.DrawRay(transform.position + Vector3.left, new Vector2(-1f, 1.41f) * enemyDistance);
        Gizmos.DrawRay(transform.position + Vector3.left, new Vector2(-1f, -1.41f) * enemyDistance);
    }
}
