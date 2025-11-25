using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState 
{
    Walk,
    Attack,
    Idle,
    Death,
    Still

}

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState; 

    [Header("Player Movement Variables")]
    private float horizontal;
    private float vertical;
    [SerializeField] private float baseSpeedMove = 4f;
    [SerializeField] private float speedMove = 0;
    public float upgradeSpeedMove = 1; 
    [SerializeField] private float sprintSpeed = 5;
    public float baseknockbackForce = 10f;
    public float upgradeKnockback = 0;
    public float knockBackForce = 0; 
    private Vector3 initialPos = new Vector3(-6f, -0.5f);
    

    private Rigidbody2D body;

    [Header("Attack movements")]
    public GameObject sword;
    public GameObject slashCollider;     
    public int baseSwordPower = 4;
    public int upgradeSwordPower = 0;
    public int swordPower = 0; 
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float attackCooldown;
    public static bool windSlash = false; 


    [Header("Abilities")]
    [SerializeField] private bool canMove = false;
    [SerializeField] private bool canSprint = true;
    public bool _canAttack = true;
    [SerializeField] private bool canAttack
    {
        get
        {
            return _canAttack;
        }
        set
        {
            _canAttack = value;
            //Debug.Log($"Can attack set to {value} attack cooldown is {attackCooldown}");
        }
    }

    public int numKill = 0;       
    public bool isStill = false;
    public float stillCoolDown = 1f;
    public float stillCount = 0;
   

    [Header("References")]
    public UpgradeManager upgradeManager;
    public PlayerHealth playerHealth;
    public SoundsManager soundManager;
    [SerializeField] private DayNightManager dayNightManager;
    private NewGameScene newGameScene;
    public GameObject windSlashObj; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Finding all the references 
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        soundManager = FindFirstObjectByType<SoundsManager>();
        dayNightManager = FindFirstObjectByType<DayNightManager>();
        newGameScene = FindFirstObjectByType<NewGameScene>();

        attackCooldown = 0.32f; 
        transform.position = initialPos;
        canMove = false;
        canAttack = false; 
        sword.gameObject.SetActive(false);
        body = GetComponent<Rigidbody2D>();
        playerState = PlayerState.Still;
        playerAnimator = GetComponent<Animator>();
        UpdatingSwordMight();
        UpdatingSpeed();        

    }

    // Update is called once per frame
    void Update()
    {
        if(newGameScene == null) 
        {
            newGameScene = FindFirstObjectByType<NewGameScene>();
        }
        
        if (newGameScene.isStarted && newGameScene.isEnded) 
        {
            canMove = true;
            canAttack = true;
            newGameScene.isEnded = false;
        }

        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Mouse0)) && playerState != PlayerState.Attack) 
        {
            if (canAttack)
            {
                soundManager.PlaySoundFXClip("SlashSword");
                playerState = PlayerState.Attack;                
            }
        }

        if(dayNightManager == null) 
        {
            dayNightManager = FindFirstObjectByType<DayNightManager>();
        }

        if (windSlash && playerState != PlayerState.Attack) 
        {
            if (canAttack)
            {
                soundManager.PlaySoundFXClip("WindSlash");
                playerState = PlayerState.Attack;
                WindSlashAttack();
                windSlash = false; 
            }
        }
                  
    }

    private void FixedUpdate()
    {
        HandleAction(playerState);                     
    }

    public void HandleAction(PlayerState playerState) 
    {
        // Action depens on the player state
        switch (playerState) 
        {
            case PlayerState.Still:
                PlayerMovement();                 
                break;
            case PlayerState.Walk:
                PlayerMovement();
                playerAnimator.SetBool("isWalking", true);
                break;
            case PlayerState.Attack:
                //body.linearVelocity = new Vector2(0f, 0f); 
                Attack();
                break;
            case PlayerState.Idle:
                break;
            case PlayerState.Death:
                canAttack = true;
                playerAnimator.SetBool("isDead", false);
                //playerAnimator.SetBool("isAttacking", false);                
                break; 
        }
    }

    public void PlayerMovement() 
    {
        if (canMove)
        {
            body.linearVelocity = new Vector2(horizontal * speedMove, vertical * speedMove);
        }

        // If the velocity is greater than 0 the player moves, if not stay still
        if (body.linearVelocity.magnitude > 0)
        {
            if (playerState != PlayerState.Attack)
            {              
                playerState = PlayerState.Walk;                
            }

            if (body.linearVelocityX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            else if (body.linearVelocityX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }

        }
        else
        {
            playerState = PlayerState.Still;
            playerAnimator.SetBool("isWalking", false);
        }
    }

    public void Attack() 
    {
        if (canAttack)
        {
            canAttack = false;
            sword.gameObject.SetActive(true);
            slashCollider.gameObject.SetActive(true);
            playerAnimator.Play("FSkyAttackAnimation");            
            Invoke("SaveSword", attackCooldown);
        }
    }
    

    public void SaveSword() 
    {        
        sword.gameObject.SetActive(false);
        slashCollider.gameObject.SetActive(false); 
        canAttack = true;       
        playerState = PlayerState.Walk; 
    }

    public void UpdatingSwordMight() 
    {
        swordPower = baseSwordPower + upgradeSwordPower;
    }

    public void UpdatingSpeed() 
    {
        speedMove = baseSpeedMove + upgradeSpeedMove; 
    }      
    
    public void UpdatingKnockback() 
    {
        knockBackForce = baseknockbackForce + upgradeKnockback; 
    }
    
    public void SetStartingPosition() 
    {
        transform.position = initialPos;
        transform.localScale = new Vector3(1, 1, 1);       
    }

    public void ResetValues() 
    {
        // Reset values when start a new game
        numKill = 0;
        transform.position = initialPos;
        upgradeSwordPower = 0;
        upgradeSpeedMove = 0;
        upgradeKnockback = 0; 
        UpdatingSwordMight();
        UpdatingSpeed();
        UpdatingKnockback();
        playerHealth.SettingInitialStats(); 
    }
     
    public void FreezePlayer() 
    {
        canMove = false;
        canAttack = false;
    }
   
    public void PlayerDeath() 
    {
        playerAnimator.SetBool("isDead", true);        
        playerState = PlayerState.Death;
        
    }

    public void WindSlashAttack() 
    {
        GameObject windSlashTemp = Instantiate(windSlashObj, transform.position, Quaternion.identity);

        // This change the direction and the x scale of the wind depending on where the player is facing (Right or left)
        // True if its facing right and false if it's facing left
        if(transform.localScale.x >= 0) 
        {
            Debug.Log("Going Right"); 
            windSlashTemp.GetComponent<WindSlashBehavior>().GetDirection(true);
        }
        else 
        {
            Debug.Log("Going Left"); 
            windSlashTemp.GetComponent<WindSlashBehavior>().GetDirection(false);
        }
            
        Debug.Log("Wind Slash!!!");
        Destroy(windSlashTemp, 5f); 
    }

    private void OnDisable()
    {
        playerState = PlayerState.Still; 
    }

}
