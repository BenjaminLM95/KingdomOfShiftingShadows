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
    [SerializeField] private PlayerState playerState;

    [Header("Player Movement Variables")]
    private float horizontal;
    private float vertical;
    [SerializeField] private float baseSpeedMove = 4f;
    [SerializeField] private float speedMove = 0;
    private float upgradeSpeedMove = 1; 
    [SerializeField] private float sprintSpeed = 5;
    public float knockBackForce = 1.5f; 
    private Vector3 initialPos = new Vector3(-6f, -0.5f);
    

    private Rigidbody2D body;

    [Header("Attack movements")]
    public GameObject sword;
    public GameObject slashCollider; 
    public bool isAttacking = false;
    public int baseSwordPower = 3;
    public int upgradeSwordPower = 0;
    public int swordPower = 0; 
    [SerializeField] private Animator playerAnimator; 


    [Header("Abilities")]
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canAttack = true;

    public int numKill = 0;    
    public bool isDead = false;
    public bool isStill = false;
    public float stillCoolDown = 1f;
    public float stillCount = 0;
   

    [Header("References")]
    public UpgradeManager upgradeManager;
    public PlayerHealth playerHealth;
    public SoundsManager soundManager;
    [SerializeField] private DayNightManager dayNightManager; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        soundManager = FindFirstObjectByType<SoundsManager>();
        dayNightManager = FindFirstObjectByType<DayNightManager>(); 
        transform.position = initialPos;    
        sword.gameObject.SetActive(false);
        body = GetComponent<Rigidbody2D>();
        playerState = PlayerState.Walk;
        playerAnimator = GetComponent<Animator>();
        UpdatingSwordMight();
        UpdatingSpeed(); 
       

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)) && playerState != PlayerState.Attack) 
        {
            Attack(); 
        }

        if(dayNightManager == null) 
        {
            dayNightManager = FindFirstObjectByType<DayNightManager>();
        }


        if (upgradeManager.isSwordUpgrade && upgradeManager.currentSwordUpgrade != null) 
        {
            if(upgradeSwordPower != upgradeManager.currentSwordUpgrade.value) 
            {
                upgradeSwordPower = upgradeManager.currentSwordUpgrade.value;
                Debug.Log(upgradeSwordPower); 
                UpdatingSwordMight(); 
            }
        }

        if (upgradeManager.isSpeedUpgrade && upgradeManager.currentSpeedUpgrade != null) 
        {
            if(upgradeSpeedMove != upgradeManager.currentSpeedUpgrade.value) 
            {
                upgradeSpeedMove = upgradeManager.currentSpeedUpgrade.value;
                Debug.Log(upgradeSpeedMove);
                UpdatingSpeed(); 
            }
        }


        if(playerHealth.healthSystem.health <= 0)
        {
            playerState = PlayerState.Death;            
            canAttack = true;
            playerAnimator.SetBool("isAttacking", false);
            isDead = true; 

        }

        
    }

    private void FixedUpdate()
    {
        


        if (canMove)
        body.linearVelocity = new Vector2(horizontal * speedMove, vertical * speedMove); 

        if(body.linearVelocity.magnitude > 0) 
        {
            if (playerState != PlayerState.Attack)
            {
                if (isAttacking)
                    isAttacking = false; 

                playerState = PlayerState.Walk;
                playerAnimator.SetBool("isWalking", true);
            }

            if (body.linearVelocityX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                
            }
            else if(body.linearVelocityX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                
            }
            
        }
        else 
        {
            playerState = PlayerState.Still;
            playerAnimator.SetBool("isWalking", false);
        }

        /*
        if (playerState == PlayerState.Still)
        {
            isStill = true;
        }

        if (isStill) 
        {
            stillCount += Time.deltaTime;
            if(stillCount > stillCoolDown) 
            {
                playerState = PlayerState.Idle; 
            }
            Debug.Log(stillCount); 
        }
        else 
        {
            stillCount = 0; 
        }*/

        
    }

    public void Attack() 
    {
        if (canAttack)
        {            
            soundManager.PlaySoundFXClip("SlashSword", transform); 
            playerState = PlayerState.Attack;
            isAttacking = true;
            sword.gameObject.SetActive(true);
            slashCollider.gameObject.SetActive(true);
            playerAnimator.SetBool("isAttacking", true);                       
            canAttack = false;
            Invoke("SaveSword", 0.4f);
        }
    }

    public void SaveSword() 
    {        
        sword.gameObject.SetActive(false);
        slashCollider.gameObject.SetActive(false); 
        canAttack = true;
        playerAnimator.SetBool("isAttacking", false);
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
        isDead = false; 
    }

    public void ResetValues() 
    {
        numKill = 0;
        transform.position = initialPos;
        upgradeSwordPower = 0;
        upgradeSpeedMove = 0;
        UpdatingSwordMight();
        UpdatingSpeed();
        playerHealth.SettingInitialStats(); 
    }
     
    
   

}
