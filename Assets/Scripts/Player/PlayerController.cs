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
    public PlayerState playerState { get; private set; }

    [Header("Player Movement Variables")]
    private float horizontal;
    private float vertical;
    [SerializeField] private float baseSpeedMove = 4f;
    [SerializeField] private float speedMove = 0;
    public float upgradeSpeedMove = 1; 
    [SerializeField] private float sprintSpeed = 5;
    public float knockBackForce = 1.5f; 
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


    [Header("Abilities")]
    [SerializeField] private bool canMove = false;
    [SerializeField] private bool canSprint = true;
    private bool _canAttack = true;
    [SerializeField] private bool canAttack
    {
        get
        {
            return _canAttack;
        }
        set
        {
            _canAttack = value;
            Debug.Log($"Can attack set to {value} attack cooldown is {attackCooldown}");
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
                soundManager.PlaySoundFXClip("SlashSword", transform);
                playerState = PlayerState.Attack;                
            }
        }

        if(dayNightManager == null) 
        {
            dayNightManager = FindFirstObjectByType<DayNightManager>();
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
            Debug.Log("Attack!");
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
        UpdatingSwordMight();
        UpdatingSpeed();
        playerHealth.SettingInitialStats(); 
    }
     
    public void FreezePlayer() 
    {
        canMove = false;
        canAttack = false;
    }
   
    public void PlayerDeath() 
    {
        playerState = PlayerState.Death;
    }

}
